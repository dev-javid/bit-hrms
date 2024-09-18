import {
  BreadCrumbProps,
  PageContainer,
  PageHeader,
  PageSkeleton,
} from '@/lib/components';
import { columns } from './columns';
import {
  Button,
  Card,
  CardContent,
  ClientSideDataTable,
  useSimpleModal,
} from 'xplorer-ui';
import { useGetIncomeSourcesQuery } from '@/lib/rtk/rtk.income-sources';
import IncomeSourceForm from '../income-source-form';

const IncomeSourceList = () => {
  const { showModal, hideModal } = useSimpleModal();
  const { data, isLoading, isFetching } = useGetIncomeSourcesQuery(null);

  const breadCrumb: BreadCrumbProps = {
    title: 'Administration',
    to: './../',
    child: { title: 'Income Sources', to: '' },
  };

  const onAddIncomeSourceClick = () => {
    const form = <IncomeSourceForm onSuccess={hideModal} />;
    showModal(`Add Income Source `, form);
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title={'Income Sources'}>
        <Button variant="outline" onClick={onAddIncomeSourceClick}>
          Add Income Source
        </Button>
      </PageHeader>
      <PageSkeleton isLoading={isLoading || isFetching}>
        <Card>
          <CardContent>
            {data?.items && (
              <ClientSideDataTable data={data.items} columns={columns} />
            )}
          </CardContent>
        </Card>
      </PageSkeleton>
    </PageContainer>
  );
};

export default IncomeSourceList;
