import { AddButton, BackButton, BreadCrumbProps, PageContainer, PageHeader } from '@/lib/components';
import { getColumns } from './columns';
import { Holiday } from '@/lib/types';
import { CardContent, Card, ClientSideDataTable, useSimpleModal } from 'xplorer-ui';
import { useGetHolidaysQuery } from '@/lib/rtk/rtk.holidays';
import HolidayForm from '../holiday-form';
import { useGetLeavePolicyQuery } from '@/lib/rtk/rtk.leave-policy';
import _ from 'lodash';

const HolidayList = () => {
  const { hideModal, showModal } = useSimpleModal();
  const { data } = useGetHolidaysQuery(null);
  const { data: leavePolicy } = useGetLeavePolicyQuery(null);
  const existingHolidaysCount = () => {
    if (data?.items?.length) {
      return _.filter(data.items, { optional: false }).length + (_.some(data.items, { optional: true }) ? 1 : 0);
    }
    return 0;
  };

  const breadCrumb: BreadCrumbProps = {
    title: 'Company Options',
    to: './../',
    child: { title: 'Holidays', to: '' },
  };

  const onAddNewClick = () => {
    showModal('Add New Holiday', <HolidayForm leavePolicy={leavePolicy} existingHolidaysCount={existingHolidaysCount()} onSuccess={hideModal} />);
  };

  const onEditClick = (holiday: Holiday) => {
    showModal(
      'Edit Holiday',
      <HolidayForm leavePolicy={leavePolicy} existingHolidaysCount={existingHolidaysCount()} holiday={holiday} onSuccess={hideModal} />
    );
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title="Holidays">
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
