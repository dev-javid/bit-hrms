import { Department } from '@/lib/types';
import { FormSchemaType } from './schema';

const useDefaultValues = (department?: Department) => {
  const defaultValues: FormSchemaType = {
    name: '',
    departmentId: 0,
  };

  if (department) {
    defaultValues.name = department.name;
    defaultValues.departmentId = department.departmentId;
  }

  return defaultValues;
};

export default useDefaultValues;
