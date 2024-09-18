import { useGetLeavePolicyQuery } from '@/lib/rtk/rtk.leave-policy';
import { FormSchemaType } from './schema';

const useDefaultValues = () => {
  const defaultValues = {
    casualLeaves: '',
    earnedLeavesPerMonth: '',
    holidays: '',
  } as unknown as FormSchemaType;

  const { data, isLoading, isFetching } = useGetLeavePolicyQuery(null);
  if (data) {
    defaultValues.casualLeaves = data.casualLeaves;
    defaultValues.earnedLeavesPerMonth = data.earnedLeavesPerMonth;
    defaultValues.holidays = data.holidays;
  }

  return {
    defaultValues,
    isLoading: isLoading || isFetching,
  };
};

export default useDefaultValues;
