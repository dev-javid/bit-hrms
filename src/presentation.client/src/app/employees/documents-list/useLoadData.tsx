import { useGetEmployeeQuery } from '@/lib/rtk/rtk.employees';
import { useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

const useLoadData = () => {
  const navigateTo = useNavigate();
  const employeeId = useParams().employeeId as number | undefined;

  const { data: employee, isLoading, isFetching } = useGetEmployeeQuery({ employeeId: employeeId ?? 0 });

  const result = {
    employee: employee,
    isLoading: isLoading || isFetching,
  };

  useEffect(() => {
    if (!employeeId) {
      navigateTo('./../../');
    }

    if (!result.isLoading && !result.employee) {
      navigateTo('./../../');
    }
  }, [employeeId, navigateTo, result.employee, result.isLoading]);

  return result;
};

export { useLoadData };
