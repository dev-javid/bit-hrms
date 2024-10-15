import { useGetAttendanceQuery, useGetRegularizationsQuery } from '@/lib/rtk/rtk.attendance';
import { useGetEmployeeLeavesQuery } from '@/lib/rtk/rtk.employee-leaves';
import { useGetHolidaysQuery } from '@/lib/rtk/rtk.holidays';
import { DateOnly } from '@/lib/types';

export default function useLoadData(date: DateOnly, employeeId?: number) {
  const {
    data: data1,
    isLoading: isLoading1,
    isFetching: isFetching1,
  } = useGetAttendanceQuery({
    month: date.month,
    year: date.year,
    employeeId: employeeId,
  });

  const { data: data2, isLoading: isLoading2, isFetching: isFetching2 } = useGetHolidaysQuery(null);
  const {
    data: data3,
    isLoading: isLoading3,
    isFetching: isFetching3,
  } = useGetEmployeeLeavesQuery({
    employeeId: employeeId ? employeeId : undefined,
  });

  const { data: data4, isLoading: isLoading4, isFetching: isFetching4 } = useGetRegularizationsQuery({ employeeId });

  return {
    data: data1 ?? [],
    isLoading: isLoading1 || isLoading2 || isLoading3 || isLoading4 || isFetching1 || isFetching2 || isFetching3 || isFetching4,
    holidays: data2?.items ?? [],
    leaves: data3?.items ?? [],
    regularizations: data4?.items ?? [],
  };
}
