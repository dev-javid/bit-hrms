import { useGetEmployeesQuery } from '@/lib/rtk/rtk.employees';
import { useGetSalariesQuery } from '@/lib/rtk/rtk.salary';
import { DateOnly, Salary } from '@/lib/types';

export default function useLoadData(date: DateOnly) {
  const {
    data: data1,
    isLoading: isLoading1,
    isFetching: isFetching1,
  } = useGetEmployeesQuery(null);

  const {
    data: data2,
    isLoading: isLoading2,
    isFetching: isFetching2,
  } = useGetSalariesQuery({
    month: date.month,
    year: date.year,
  });

  const data: Salary[] = [];

  if (data1?.items) {
    for (const employee of data1.items) {
      const salary = data2?.items?.find(
        (salary) => salary.employeeId === employee.employeeId
      );

      data.push({
        salaryId: salary?.salaryId ?? 0,
        employeeId: employee.employeeId,
        employeeName: employee.fullName,
        month: salary?.month ?? date.month,
        amount: salary?.amount ?? 0,
        year: salary?.year ?? date.year,
        disbursedOn: salary?.disbursedOn,
        compensation: salary?.compensation ?? 0,
        salaryDudections: salary?.salaryDudections ?? [],
      });
    }
  }

  return {
    data: data,
    isLoading: isLoading1 || isLoading2 || isFetching1 || isFetching2,
  };
}
