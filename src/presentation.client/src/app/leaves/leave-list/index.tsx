import { useApproveEamployeeLeaveMutation, useDeleteEmployeeLeaveMutation, useGetEmployeeLeavesQuery } from '@/lib/rtk/rtk.employee-leaves';
import { ActionButton, BreadCrumbProps, PageContainer, PageHeader } from '@/lib/components';
import { columns } from './columns';
import { Card, CardContent, ClientSideDataTable, useSimpleModal, useSimpleConfirm, Container } from 'xplorer-ui';
import { EmployeeLeave } from '@/lib/types';
import { toast } from 'xplorer-ui';
import { useState } from 'react';
import DeclineLeaveForm from '../decline-leave-form';
import useAuth from '@/lib/hooks/use-auth';
import LeaveSummary from '@/app/home/employee/leave-summary';
import ApplyLeave from '../apply-leave';

const EmployeeLeaveList = () => {
  const { showConfirm } = useSimpleConfirm();
  const { showModal, hideModal } = useSimpleModal();
  const [deleteLeave] = useDeleteEmployeeLeaveMutation();
  const [approve] = useApproveEamployeeLeaveMutation();
  const [decline, setDecline] = useState<EmployeeLeave | undefined>(undefined);
  const { user } = useAuth();
  const { data, isLoading, isFetching } = useGetEmployeeLeavesQuery({
    employeeId: user.isCompanyAdmin ? undefined : user.employeeId,
  });

  const breadCrumb: BreadCrumbProps = {
    title: 'Leaves',
    to: '',
  };

  const onDeleteClick = async (data: EmployeeLeave) => {
    const confirmed = await showConfirm('Delete Leave', 'Are you sure you want to delete this leave?');

    if (confirmed) {
      const response = await deleteLeave({
        employeeLeaveId: data.employeeLeaveId,
      });

      if (!('error' in response)) {
        toast({
          variant: 'primary',
          title: 'Success ',
          description: 'Leave deleted successfully',
        });
      }
    }
  };

  const onSummaryClick = (employee: EmployeeLeave) => {
    const summary = <LeaveSummary employeeId={employee.employeeId} hideActions />;
    showModal(`Leave Summary (${employee.employeeName})`, summary);
  };

  const onApplyLeaveClick = () => {
    const summary = <ApplyLeave onSuccess={hideModal} />;
    showModal(`Apply Leave`, summary);
  };

  const onApproveClick = async (data: EmployeeLeave) => {
    const confirmed = await showConfirm('Approve Leave', 'Are you sure you want to approve this leave?');

    if (confirmed) {
      const response = await approve({
        employeeLeaveId: data.employeeLeaveId,
      });

      if (!('error' in response)) {
        toast({
          variant: 'primary',
          title: 'Success ',
          description: 'Leave approved',
        });
      }
    }
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title="Leave History">
        {user.isEmployee && <ActionButton onClick={onApplyLeaveClick} tooltip="Apply Leave" text="Apply Leave" />}
      </PageHeader>
      {decline && <DeclineLeaveForm leave={decline} onCancelDecline={() => setDecline(undefined)} />}
      <Container isLoading={isLoading || isFetching}>
        <Card>
          <CardContent>
            {data && (
              <ClientSideDataTable
                data={data?.items}
                columns={columns}
                onDelete={onDeleteClick}
                isDeleteDisabled={(data) => data.status != 'Pending' || user.isCompanyAdmin}
                otherActions={[
                  {
                    action: 'Approve',
                    onAction: (data) => onApproveClick(data),
                    hidden: (data) => data.status != 'Pending' || !user.isCompanyAdmin,
                  },
                  {
                    action: 'Decline',
                    onAction: (data) => setDecline(data),
                    hidden: (data) => data.status != 'Pending' || !user.isCompanyAdmin,
                  },
                  {
                    action: 'Leave Summary',
                    onAction: onSummaryClick,
                  },
                ]}
              />
            )}
          </CardContent>
        </Card>
      </Container>
    </PageContainer>
  );
};

export default EmployeeLeaveList;
