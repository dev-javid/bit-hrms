import { useGetDepartmentsQuery } from '@/lib/rtk/rtk.departments';
import { AddButton, BackButton, BreadCrumbProps, PageContainer, PageHeader, PageSkeleton } from '@/lib/components';
import { Department } from '@/lib/types';
import { Card, CardContent, ClientSideDataTable, useSimpleModal } from 'xplorer-ui';
import DepartmentForm from '../department-form';
import { getColumns } from './columns';

const DepartmentList = () => {
  const { hideModal, showModal } = useSimpleModal();
  const { data, isLoading, isFetching } = useGetDepartmentsQuery(null);

  const breadCrumb: BreadCrumbProps = {
    title: 'Company Options',
    to: './../',
    child: { title: 'Departments', to: '' },
  };

  const onAddNewClick = () => {
    showModal('Add New Department', <DepartmentForm onSuccess={hideModal} />);
  };

  const onEditClick = (department: Department) => {
    showModal('Edit Department', <DepartmentForm department={department} onSuccess={hideModal} />);
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title="Departments">
        <BackButton to="./../" text="Company Options" />
        <AddButton onClick={onAddNewClick} tooltip="Add new department" />
      </PageHeader>
      <PageSkeleton isLoading={isLoading || isFetching}>
        <Card>
          <CardContent>{data && <ClientSideDataTable data={data?.items} columns={getColumns(onEditClick)} />}</CardContent>
        </Card>
      </PageSkeleton>
    </PageContainer>
  );
};

export default DepartmentList;
