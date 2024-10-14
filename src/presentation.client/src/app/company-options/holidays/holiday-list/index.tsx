import { AddButton, BackButton, BreadCrumbProps, PageContainer, PageHeader } from '@/lib/components';
import { getColumns } from './columns';
import { Holiday } from '@/lib/types';
import { CardContent, Card, ClientSideDataTable, useSimpleModal } from 'xplorer-ui';
import { useGetHolidaysQuery } from '@/lib/rtk/rtk.holidays';
import HolidayForm from '../holiday-form';

const HolidayList = () => {
  const { hideModal, showModal } = useSimpleModal();
  const { data } = useGetHolidaysQuery(null);

  const breadCrumb: BreadCrumbProps = {
    title: 'Company Options',
    to: './../',
    child: { title: 'Holidays', to: '' },
  };

  const onAddNewClick = () => {
    showModal('Add New Holiday', <HolidayForm onSuccess={hideModal} />);
  };

  const onEditClick = (holiday: Holiday) => {
    showModal('Edit Holiday', <HolidayForm holiday={holiday} onSuccess={hideModal} />);
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title={'Holidays'}>
        <BackButton to="./../" text="Company Options" />
        <AddButton onClick={onAddNewClick} tooltip="Add new holiday" />
      </PageHeader>
      <Card>
        <CardContent>{data && <ClientSideDataTable data={data?.items} columns={getColumns(onEditClick)} />}</CardContent>
      </Card>
    </PageContainer>
  );
};

export default HolidayList;
