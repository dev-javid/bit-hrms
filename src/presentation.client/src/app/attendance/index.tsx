import { PageContainer, PageHeader, BreadCrumbProps, MonthsDropdown, EmployeeDropdown, ActionButton } from '@/lib/components';
import { Card, CardContent, ClientSideDataTable, Container, useSimpleModal } from 'xplorer-ui';
import useLoadData from './useLoadData';
import { useMemo, useState } from 'react';
import useAuth from '@/lib/hooks/use-auth';
import { AttendanceRegularization, DateOnly, Employee, EmployeeLeave, Holiday, InOutTiming, InOutTimingStatus } from '@/lib/types';
import { useLocation } from 'react-router-dom';
import { isWeekOff, isWithinDateRange } from '@/lib/utils';
import { getDaysInMonth } from 'date-fns';
import { getColumns } from './columns';
import RegularizationForm from './regularization/regularization-form';

const MonthlyAttendance = () => {
  const { user } = useAuth();
  const [employee, setEmployee] = useState(((useLocation().state ?? {}) as { employee: Employee }).employee);
  const [date, setDate] = useState(DateOnly.firstDayOfCurrentMonth());
  const { data, holidays, leaves, regularizations, isLoading } = useLoadData(date, employee?.employeeId ?? user?.employeeId);

  const { showModal } = useSimpleModal();
  const attendance = useMemo(
    () => (employee ? getMonthData(data, holidays, leaves, date, employee.employeeId, regularizations) : []),
    [data, holidays, leaves, employee, date, regularizations]
  );

  const onRegularizeClick = (inoutTiming: InOutTiming) => {
    const summary = <RegularizationForm inoutTiming={inoutTiming} />;
    showModal(`Regularize (${inoutTiming.date.asDateOnly().toDayString()}) `, summary);
  };

  const breadCrumb: BreadCrumbProps = employee
    ? {
        title: 'Attendance',
        to: './',
        state: {},
        child: {
          title: employee.fullName,
          to: '',
        },
      }
    : {
        title: user.isEmployee ? 'My Attendance' : 'Attendance',
        to: '',
      };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title={date.toMonthString()}>
        <ActionButton text="Regularizations" to="regularizations" tooltip="View regularizations" state={{ employee }} />
        {user.isCompanyAdmin && (
          <EmployeeDropdown onEmployeeSelect={(e) => setEmployee(e)} selectedEmployeeId={employee?.employeeId ?? user?.employeeId} />
        )}
        {employee && <MonthsDropdown onMonthChange={setDate} defaultValue={date} referenceDate={employee.dateOfJoining.asDateOnly() ?? date} />}
      </PageHeader>
      <Container isLoading={isLoading} rows={30}>
        <Card>
          <CardContent>
            <>{attendance && <ClientSideDataTable pageSize={31} data={attendance} columns={getColumns(onRegularizeClick)} />}</>
          </CardContent>
        </Card>
      </Container>
    </PageContainer>
  );
};

const getMonthData = (
  inOutTimings: InOutTiming[],
  holidays: Holiday[],
  leaves: EmployeeLeave[],
  dateOnly: DateOnly,
  employeeId: number,
  regularizations: AttendanceRegularization[]
): InOutTiming[] => {
  const now = new Date();
  const year = dateOnly.year;
  const month = dateOnly.month;

  const numDays = month == now.getMonth() + 1 ? now.getDate() + 1 : getDaysInMonth(dateOnly.toDate());

  const datesArray: string[] = [];
  for (let day = 1; day < numDays; day++) {
    datesArray.push(DateOnly.fromParts(year, month, numDays - day).toDateOnlyISOString());
  }

  return datesArray.map((x) => {
    const inOutTiming = inOutTimings.find((i) => i.date === x);
    const regularization = regularizations.find((r) => r.date == x);

    let status: InOutTimingStatus = 'ClockedIn';
    if (isWeekOff(x)) {
      status = 'WeeklyOff';
    } else if (holidays.find((h) => h.date == x)) {
      status = 'Holiday';
    } else if (leaves.find((h) => h.status === 'Approved' && isWithinDateRange(h.from, h.to, x))) {
      status = 'OnLeave';
    } else if (inOutTiming?.clockOutTime) {
      status = 'Approved';
    } else if (regularization) {
      status = 'RegularizationRequested';
    } else if (inOutTiming?.clockInTime && !inOutTiming?.clockOutTime) {
      status = x == DateOnly.fromDate(now).toDateOnlyISOString() ? 'ClockedIn' : 'ClockOutMissing';
    } else if (!inOutTiming?.clockInTime) {
      status = 'ClockInMissing';
    }

    return {
      employeeId: employeeId,
      status: status,
      clockInTime: inOutTiming?.clockInTime ?? '',
      clockOutTime: inOutTiming?.clockOutTime ?? '',
      regularization: regularization,
      date: x,
    };
  });
};

export default MonthlyAttendance;
