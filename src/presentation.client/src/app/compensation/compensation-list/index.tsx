import { PageContainer, PageHeader, PageSkeleton, BreadCrumbProps, EmployeeDropdown } from '@/lib/components';

import { columns } from './columns';
import { Card, CardContent, Button, ClientSideDataTable, useSimpleModal } from 'xplorer-ui';
import { useLocation } from 'react-router-dom';
import { useGetCompensationQuery } from '@/lib/rtk/rtk.salary';
import _ from 'lodash';
import { useState } from 'react';
import useAuth from '@/lib/hooks/use-auth';
import { useEffect } from 'react';
import { Employee } from '@/lib/types';
import CompensationForm from '../compensation-form';

const CompensationList = () => {
  const [employee, setEmploee] = useState<Employee | undefined>(undefined);
  const { user } = useAuth();
  const { showModal, hideModal } = useSimpleModal();
  const { data, isLoading, isFetching } = useGetCompensationQuery({
    employeeId: employee?.employeeId,
  });

  const locationState = useLocation().state as { employee: Employee };
  useEffect(() => {
    setEmploee(locationState?.employee);
  }, [locationState]);

  const breadCrumb: BreadCrumbProps = employee
    ? {
        title: 'Compensation',
        to: './',
        child: {
          title: employee.fullName,
          to: '',
        },
      }
    : {
        title: 'My Compensation',
        to: '',
      };

  const compensations = _.sortBy(data, ['effectiveFrom'])
    .reverse()
    .map((c) => ({
      ...c,
      active: false,
    }));

  if (compensations.length) {
    compensations[0].active = true;
  }

  const onAddCompensationClick = () => {
    showModal('Add Compensation', <CompensationForm employee={employee!} onSuccess={hideModal} />);
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title="Compensation">
        {user.isCompanyAdmin && (
          <>
            <EmployeeDropdown onEmployeeSelect={(e) => setEmploee(e)} selectedEmployeeId={employee?.employeeId} />
            {employee && (
              <Button variant="outline" onClick={onAddCompensationClick}>
                Add New Compensation
              </Button>
            )}
          </>
        )}
      </PageHeader>
      <PageSkeleton isLoading={isLoading || isFetching}>
        <Card>
          <CardContent>
            <ClientSideDataTable data={compensations} columns={columns} />
          </CardContent>
        </Card>
      </PageSkeleton>
    </PageContainer>
  );
};

export default CompensationList;
