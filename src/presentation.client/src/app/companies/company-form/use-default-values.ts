import { Company } from '@/lib/types';
import { FormSchemaType } from './schema';

const useDefaultValues = (company?: Company) => {
  const defaultValues: FormSchemaType = {
    companyId: company?.companyId ?? 0,
    name: company?.name ?? '',
    email: company?.email ?? '',
    administratorName: company?.administratorName ?? '',
    phoneNumber: company?.phoneNumber ?? '',
    address: company?.address ?? '',
  } as never as FormSchemaType;

  return {
    defaultValues,
  };
};

export default useDefaultValues;
