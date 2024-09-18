import { Holiday } from '@/lib/types';
import { FormSchemaType } from './schema';

const useDefaultValues = (holiday?: Holiday) => {
  const defaultValues: FormSchemaType = {
    holidayId: 0,
    date: '',
    name: '',
    optional: false,
  } as never as FormSchemaType;

  if (holiday) {
    defaultValues.holidayId = holiday.holidayId;
    defaultValues.name = holiday.name;
    defaultValues.date = holiday.date.asDateOnly().toDate();
    defaultValues.optional = holiday.optional;
  }

  return defaultValues;
};

export default useDefaultValues;
