import {
  AttendanceRegularization,
  DateOnly,
  EmployeeLeave,
  Holiday,
  InOutTiming,
  InOutTimingStatus,
} from '@/lib/types';
import { getColumns } from './columns';
import { isWeekOff, isWithinDateRange } from '@/lib/utils';
import { useMemo } from 'react';
import { ClientSideDataTable, useSimpleModal } from 'xplorer-ui';
import { getDaysInMonth } from 'date-fns';
import RegularizationForm from '../../regularization/regularization-form';

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

  const numDays =
    month == now.getMonth() + 1
      ? now.getDate() + 1
      : getDaysInMonth(dateOnly.toDate());

  const datesArray: string[] = [];
  for (let day = 1; day < numDays; day++) {
    datesArray.push(
      DateOnly.fromParts(year, month, numDays - day).toDateOnlyISOString()
    );
  }

  return datesArray.map((x) => {
    const inOutTiming = inOutTimings.find((i) => i.date === x);
    const regularization = regularizations.find((r) => r.date == x);

    let status: InOutTimingStatus = 'ClockedIn';
    if (isWeekOff(x)) {
      status = 'WeeklyOff';
    } else if (holidays.find((h) => h.date == x)) {
      status = 'Holiday';
    } else if (
      leaves.find(
        (h) => h.status === 'Approved' && isWithinDateRange(h.from, h.to, x)
      )
    ) {
      status = 'OnLeave';
    } else if (inOutTiming?.clockOutTime) {
      status = 'Approved';
    } else if (regularization) {
      status = 'RegularizationRequested';
    } else if (inOutTiming?.clockInTime && !inOutTiming?.clockOutTime) {
      status =
        x == DateOnly.fromDate(now).toDateOnlyISOString()
          ? 'ClockedIn'
          : 'ClockOutMissing';
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

const AttendanceList = ({
  data,
  holidays,
  leaves,
  employeeId,
  date,
  regularizations,
}: {
  employeeId: number;
  data: InOutTiming[];
  holidays: Holiday[];
  leaves: EmployeeLeave[];
  regularizations: AttendanceRegularization[];
  date: DateOnly;
}) => {
  const { showModal } = useSimpleModal();
  const attendance = useMemo(
    () =>
      getMonthData(data, holidays, leaves, date, employeeId, regularizations),
    [data, holidays, leaves, employeeId, date, regularizations]
  );

  const onRegularizeClick = (inoutTiming: InOutTiming) => {
    const summary = <RegularizationForm inoutTiming={inoutTiming} />;
    showModal(
      `Regularize (${inoutTiming.date.asDateOnly().toDayString()}) `,
      summary
    );
  };

  return (
    <>
      {attendance && (
        <ClientSideDataTable
          pageSize={31}
          data={attendance}
          columns={getColumns(onRegularizeClick)}
        />
      )}
    </>
  );
};

export default AttendanceList;
