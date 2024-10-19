import { AddButton, BreadCrumbProps, PageContainer, PageHeader } from '@/lib/components';
import useColumns from './useColumns';
import { Card, CardContent, ClientSideDataTable, Container } from 'xplorer-ui';
import { useGetEmployeesQuery } from '@/lib/rtk/rtk.employees';

const EmployeesList = () => {
  const { data, isLoading, isFetching } = useGetEmployeesQuery(null);
  const columns = useColumns();

  const breadCrumb: BreadCrumbProps = {
    title: 'Employees',
    to: './../',
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title="Employees">
        <AddButton to="0" tooltip="Add new employee" />
      </PageHeader>
      <Container isLoading={isLoading || isFetching}>
        <Card>
          <CardContent>{data && <ClientSideDataTable data={data?.items} columns={columns} />}</CardContent>
        </Card>
      </Container>
    </PageContainer>
  );
};

export default EmployeesList;
