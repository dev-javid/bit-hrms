import { AddButton, BreadCrumbProps, PageContainer, PageHeader, PageSkeleton } from '@/lib/components';
import { columns } from './columns';
import { Card, CardContent, ClientSideDataTable } from 'xplorer-ui';
import { useGetEmployeesQuery } from '@/lib/rtk/rtk.employees';

const EmployeesList = () => {
  const { data, isLoading, isFetching } = useGetEmployeesQuery(null);

  const breadCrumb: BreadCrumbProps = {
    title: 'Employees',
    to: './../',
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title="Employees">
        <AddButton to="0" tooltip="Add new employee" />
      </PageHeader>
      <PageSkeleton isLoading={isLoading || isFetching}>
        <Card>
          <CardContent>{data && <ClientSideDataTable data={data?.items} columns={columns} />}</CardContent>
        </Card>
      </PageSkeleton>
    </PageContainer>
  );
};

export default EmployeesList;
