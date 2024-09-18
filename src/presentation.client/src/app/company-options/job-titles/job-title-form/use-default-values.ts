import { JobTitle } from '@/lib/types';
import { FormSchemaType } from './schema';
import { useGetDepartmentsQuery } from '@/lib/rtk/rtk.departments';

const useDefaultValues = (jobTitle?: JobTitle) => {
  const defaultValues: FormSchemaType = {
    name: '',
    departmentId: 0,
    jobTitleId: 0,
  };

  if (jobTitle) {
    defaultValues.name = jobTitle.name;
    defaultValues.departmentId = jobTitle.departmentId;
    defaultValues.jobTitleId = jobTitle.jobTitleId;
  }

  const { data, isLoading, isFetching } = useGetDepartmentsQuery(null);

  return {
    defaultValues,
    departments: data?.items ?? [],
    isLoading: isLoading || isFetching,
  };
};

export default useDefaultValues;
