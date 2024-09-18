import {
  PageContainer,
  PageHeader,
  PageSkeleton,
  BreadCrumbProps,
  MonthsDropdown,
  EmployeeDropdown,
} from '@/lib/components';
import { Button, Card, CardContent } from 'xplorer-ui';
import AttendanceList from './attendence-list';
import useLoadData from './useLoadData';
import { useState } from 'react';
import useAuth from '@/lib/hooks/use-auth';
import { DateOnly, Employee } from '@/lib/types';
import { Link, useLocation } from 'react-router-dom';

const MonthlyAttendance = () => {
  const [employee, setEmployee] = useState(
    ((useLocation().state ?? {}) as { employee: Employee }).employee
  );
  const [date, setDate] = useState(DateOnly.firstDayOfCurrentMonth());
  const { data, holidays, leaves, regularizations, isLoading } = useLoadData(
    date,
    employee?.employeeId
  );

  const { user } = useAuth();

  const breadCrumb: BreadCrumbProps = employee
    ? {
        title: 'Attendance',
        to: './',
        child: {
          title: employee.fullName,
          to: '',
        },
      }
    : {
        title: 'My Attendance',
        to: '',
      };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title={date.toMonthString()}>
        <Button asChild variant="link">
          <Link to="regularizations" state={{ employee }}>
            Regularizations
          </Link>
        </Button>
        {user.isCompanyAdmin && (
          <EmployeeDropdown
            onEmployeeSelect={(e) => setEmployee(e)}
            selectedEmployeeId={employee?.employeeId}
          />
        )}
        <MonthsDropdown onMonthChange={setDate} defaultValue={date} />
      </PageHeader>
      <PageSkeleton isLoading={isLoading} rows={30}>
        <Card>
          <CardContent>
            <AttendanceList
              data={data}
              holidays={holidays}
              leaves={leaves}
              employeeId={employee?.employeeId ?? 0}
              date={date}
              regularizations={regularizations}
            />
          </CardContent>
        </Card>
      </PageSkeleton>
    </PageContainer>
  );
};

export default MonthlyAttendance;
