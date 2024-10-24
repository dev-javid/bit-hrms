import { useGetDepartmentsQuery } from '@/lib/rtk/rtk.departments';
import { FormSchemaType } from './schema';
import { useGetEmployeeQuery } from '@/lib/rtk/rtk.employees';
import { useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

const useDefaultValues = () => {
  const navigateTo = useNavigate();
  const employeeId = (useParams().employeeId as number | undefined) || 0;

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

  const {
    data: employee,
    isLoading: employeeIsLoading,
    isFetching: employeeIsFetching,
  } = useGetEmployeeQuery({ employeeId: employeeId ?? 0 }, { skip: employeeId == 0 });

  const { data: departments, isLoading: departmentsIsLoading, isFetching: departmentsIsFetching } = useGetDepartmentsQuery(null);

  const result = {
    employee: employee,
    isLoading: employeeIsLoading || employeeIsFetching || departmentsIsLoading || departmentsIsFetching,
  };

  useEffect(() => {
    if (employeeId > 0 && !result.isLoading && !result.employee) {
      navigateTo('./../../');
    }
  }, [employeeId, navigateTo, result.employee, result.isLoading]);

  if (result.employee) {
    defaultValues.departmentId = result.employee.departmentId;
    defaultValues.jobTitleId = result.employee.jobTitleId;
    defaultValues.pan = result.employee.pan;
    defaultValues.aadhar = result.employee.aadhar;
    defaultValues.address = result.employee.address;
    defaultValues.city = result.employee.city;
    defaultValues.companyEmail = result.employee.companyEmail;
    defaultValues.dateOfBirth = result.employee.dateOfBirth.asDateOnly().toDate();
    defaultValues.dateOfJoining = result.employee.dateOfJoining.asDateOnly().toDate();
    defaultValues.fatherName = result.employee.fatherName;
    defaultValues.firstName = result.employee.firstName;
    defaultValues.lastName = result.employee.lastName;
    defaultValues.personalEmail = result.employee.personalEmail;
    defaultValues.phoneNumber = result.employee.phoneNumber;
  }
  return {
    defaultValues: defaultValues,
    departments: departments?.items ?? [],
    isLoading: result.isLoading,
    fullName: employee?.fullName,
  };
};

export default useDefaultValues;
