import { BreadCrumbProps, MonthsDropdown, PageContainer, PageHeader } from '@/lib/components';
import { columns } from './columns';
import { Card, CardContent, ClientSideDataTable, Container } from 'xplorer-ui';
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
      <Container isLoading={isLoading}>
        <Card>
          <CardContent>
            <ClientSideDataTable data={data} columns={columns} />
          </CardContent>
        </Card>
      </Container>
    </PageContainer>
  );
};

export default SalaryList;
