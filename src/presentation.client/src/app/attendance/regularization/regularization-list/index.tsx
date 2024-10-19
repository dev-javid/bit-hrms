import { BackButton, BreadCrumbProps, EmployeeDropdown, PageContainer, PageHeader } from '@/lib/components';
import { getColumns } from './columns';
import { AttendanceRegularization, Employee } from '@/lib/types';
import { useLocation } from 'react-router-dom';
import { CardContent, Card, ClientSideDataTable, useSimpleConfirm, toast, Container } from 'xplorer-ui';
import { useApproveRegularizationMutation, useGetRegularizationsQuery } from '@/lib/rtk/rtk.attendance';
import { useState } from 'react';
import useAuth from '@/lib/hooks/use-auth';

const RegularizationList = () => {
  const { user } = useAuth();
  const { showConfirm } = useSimpleConfirm();
  const [approve] = useApproveRegularizationMutation();
  const [employee, setEmployee] = useState(((useLocation().state ?? {}) as { employee: Employee }).employee);
  const { data, isLoading, isFetching } = useGetRegularizationsQuery({
    employeeId: employee?.employeeId,
  });

  const breadCrumb: BreadCrumbProps = employee
    ? {
        title: 'Attendance',
        to: './../',
        child: {
          title: employee.fullName,
          to: '/app/employees/' + employee.employeeId,
          child: {
            title: 'Regularizations',
            to: '',
          },
        },
      }
    : {
        title: 'Attendance',
        to: './../',
        child: {
          title: 'Regularizations',
          to: '',
        },
      };

  const onApproveClick = async (regularization: AttendanceRegularization) => {
    const ok = await showConfirm(
      `Approve Regularization: ${regularization.date.asDateOnly().toDayString()}`,
      'Are you sure you want to approve this regularization?'
    );

    if (ok) {
      const response = await approve({
        attendanceRegularizationId: regularization.attendanceRegularizationId,
      });

      if (!('error' in response)) {
        toast({
          variant: 'primary',
          title: 'Success ',
          description: 'Regularization approved.',
        });
      }
    }
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title="Regularizations">
        <BackButton to="./../" text="Attendance" state={{ employee }} />
        {user.isCompanyAdmin && <EmployeeDropdown onEmployeeSelect={(e) => setEmployee(e)} selectedEmployeeId={employee?.employeeId} />}
      </PageHeader>
      <Card>
        <CardContent>
          <Container isLoading={isLoading || isFetching} rows={30}>
            {data && <ClientSideDataTable data={data?.items} columns={getColumns(onApproveClick, user)} />}
          </Container>
        </CardContent>
      </Card>
    </PageContainer>
  );
};

export default RegularizationList;
