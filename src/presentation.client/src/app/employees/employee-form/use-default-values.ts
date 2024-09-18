import { Employee } from '@/lib/types';
import { useLocation } from 'react-router-dom';
import { useGetDepartmentsQuery } from '@/lib/rtk/rtk.departments';
import { FormSchemaType } from './schema';

const useDefaultValues = () => {
  const locationState = useLocation().state as Employee;

  const defaultValues: FormSchemaType = {
    departmentId: '',
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    dateOfJoining: '',
    fatherName: '',
    jobTitleId: '',
    phoneNumber: '',
    companyEmail: '',
    personalEmail: '',
    address: '',
    city: '',
    pan: '',
    aadhar: '',
  } as never as FormSchemaType;

  if (locationState) {
    defaultValues.departmentId = locationState.departmentId;
  }

  const { data, isLoading, isFetching } = useGetDepartmentsQuery(null);

  return {
    defaultValues,
    departments: data?.items ?? [],
    isLoading: isLoading || isFetching,
  };
};

export default useDefaultValues;
