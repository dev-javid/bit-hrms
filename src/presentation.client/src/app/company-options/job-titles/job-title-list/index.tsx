import {
  BreadCrumbProps,
  PageContainer,
  PageHeader,
  PageSkeleton,
} from '@/lib/components';
import { columns } from './columns';
import { JobTitle } from '@/lib/types';
import { useParams } from 'react-router-dom';
import {
  Button,
  Card,
  CardContent,
  ClientSideDataTable,
  useSimpleModal,
} from 'xplorer-ui';
import { useGetJobTitlesQuery } from '@/lib/rtk/rtk.job-titles';
import JobTitleForm from '../job-title-form';

const JobTitleList = () => {
  const { hideModal, showModal } = useSimpleModal();
  const departmentId = useParams().departmentId ?? 0;
  const { data, isLoading, isFetching } = useGetJobTitlesQuery({
    departmentId: departmentId as number,
  });

  const breadCrumb: BreadCrumbProps = {
    title: 'Administration',
    to: './../',
    child: { title: 'Job Titles', to: '' },
  };

  const onAddNewClick = () => {
    showModal('Add New Job Title', <JobTitleForm onSuccess={hideModal} />);
  };

  const onEditClick = (jobTitle: JobTitle) => {
    showModal(
      'Edit Job Title',
      <JobTitleForm jobTitle={jobTitle} onSuccess={hideModal} />
    );
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title="Job Titles">
        <Button variant="outline" onClick={onAddNewClick}>
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
                onEdit={onEditClick}
              />
            )}
          </CardContent>
        </Card>
      </PageSkeleton>
    </PageContainer>
  );
};

export default JobTitleList;
