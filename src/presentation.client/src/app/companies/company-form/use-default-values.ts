import { Company } from '@/lib/types';
import { useLocation } from 'react-router-dom';
import { FormSchemaType } from './schema';

const useDefaultValues = () => {
  const defaultValues: FormSchemaType = {
    companyId: 0,
    name: '',
    email: '',
    administratorName: '',
    financialMonth: '',
    phoneNumber: '',
  } as never as FormSchemaType;

  const locationState = useLocation().state as Company;

  if (locationState) {
    defaultValues.name = locationState.name;
    defaultValues.companyId = locationState.companyId;
  }

  return {
    defaultValues,
    isLoading: false,
  };
};

export default useDefaultValues;
