import { BreadCrumbProps, MonthsDropdown, PageContainer, PageHeader, PageSkeleton } from '@/lib/components';
import { columns } from './columns';
import { Card, CardContent, ClientSideDataTable } from 'xplorer-ui';
import useLoadData from './useLoadData';
import { useState } from 'react';
import { DateOnly } from '@/lib/types';

const SalaryList = () => {
  const [date, setDate] = useState(DateOnly.firstDayOfCurrentMonth());

  const { data, company, isLoading } = useLoadData(date);

  const breadCrumb: BreadCrumbProps = {
    title: 'Salary Management',
    to: '',
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title={date.toMonthString()}>
        {company && (
          <MonthsDropdown onMonthChange={setDate} defaultValue={DateOnly.firstDayOfCurrentMonth()} referenceDate={company.createdOn.asDateOnly()} />
        )}
      </PageHeader>
      <PageSkeleton isLoading={isLoading}>
        <Card>
          <CardContent>
            <ClientSideDataTable data={data} columns={columns} />
          </CardContent>
        </Card>
      </PageSkeleton>
    </PageContainer>
  );
};

export default SalaryList;
