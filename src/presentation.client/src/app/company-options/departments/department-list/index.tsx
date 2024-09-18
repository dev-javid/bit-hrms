import { useGetDepartmentsQuery } from '@/lib/rtk/rtk.departments';
import {
  BreadCrumbProps,
  PageContainer,
  PageHeader,
  PageSkeleton,
} from '@/lib/components';
import { columns } from './columns';
import { Department } from '@/lib/types';
import {
  Button,
  Card,
  CardContent,
  ClientSideDataTable,
  useSimpleModal,
} from 'xplorer-ui';
import DepartmentForm from '../department-form';

const DepartmentList = () => {
  const { hideModal, showModal } = useSimpleModal();
  const { data, isLoading, isFetching } = useGetDepartmentsQuery(null);

  const breadCrumb: BreadCrumbProps = {
    title: 'Administration',
    to: './../',
    child: { title: 'Departments', to: '' },
  };

  const onAddNewClick = () => {
    showModal('Add New Department', <DepartmentForm onSuccess={hideModal} />);
  };

  const onEditNewClick = (department: Department) => {
    showModal(
      'Edit Department',
      <DepartmentForm department={department} onSuccess={hideModal} />
    );
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title="Departments">
        <Button onClick={onAddNewClick} variant="outline">
          Add new
        </Button>
      </PageHeader>
      <PageSkeleton isLoading={isLoading || isFetching}>
        <Card>
          <CardContent>
            {data && (
              <ClientSideDataTable
                data={data?.items}
                columns={columns}
                onEdit={onEditNewClick}
              />
            )}
          </CardContent>
        </Card>
      </PageSkeleton>
    </PageContainer>
  );
};

export default DepartmentList;
