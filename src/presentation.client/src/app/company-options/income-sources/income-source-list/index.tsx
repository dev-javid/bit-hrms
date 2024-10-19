import { AddButton, BackButton, BreadCrumbProps, PageContainer, PageHeader } from '@/lib/components';
import { useColumns } from './columns';
import { Card, CardContent, ClientSideDataTable, Container, useSimpleModal } from 'xplorer-ui';
import { useGetIncomeSourcesQuery } from '@/lib/rtk/rtk.income-sources';
import IncomeSourceForm from '../income-source-form';
import { IncomeSource } from '@/lib/types';

const IncomeSourceList = () => {
  const { showModal, hideModal } = useSimpleModal();
  const { data, isLoading, isFetching } = useGetIncomeSourcesQuery(null);

  const breadCrumb: BreadCrumbProps = {
    title: 'Company Options',
    to: './../',
    child: { title: 'Income Sources', to: '' },
  };

  const onAddNewClick = () => {
    const form = <IncomeSourceForm onSuccess={hideModal} />;
    showModal(`Add Income Source `, form);
  };

  const onEditClick = (incomeSource: IncomeSource) => {
    const form = <IncomeSourceForm onSuccess={hideModal} incomeSource={incomeSource} />;
    showModal(`Edit Income Source `, form);
  };

  const columns = useColumns(onEditClick);

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title={'Income Sources'}>
        <BackButton to="./../" text="Company Options" />
        <AddButton onClick={onAddNewClick} tooltip="Add new income source" />
      </PageHeader>
      <Container isLoading={isLoading || isFetching}>
        <Card>
          <CardContent>{data?.items && <ClientSideDataTable data={data.items} columns={columns} />}</CardContent>
        </Card>
      </Container>
    </PageContainer>
  );
};

export default IncomeSourceList;
