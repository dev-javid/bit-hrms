import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import FormSchema, { FormSchemaType } from './schema';
import { useAddHolidayMutation, useUpdateHolidayMutation } from '@/lib/rtk/rtk.holidays';
import { toast } from 'xplorer-ui';
import { DateOnly } from '@/lib/types';

const useFormMethods = (defaultValues: FormSchemaType, onSuccess: () => void) => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: defaultValues,
  });

  const [add] = useAddHolidayMutation();
  const [update] = useUpdateHolidayMutation();

  async function onSubmit(data: FormSchemaType) {
    const operation = data?.holidayId ? update : add;
    const response = await operation({
      ...data,
      date: DateOnly.fromDate(data.date).toDateOnlyISOString(),
      optional: !!data.optional,
    });

    if (!('error' in response)) {
      toast({
        variant: 'primary',
        title: 'Success ',
        description: 'Holiday details saved',
      });
      onSuccess();
    }
  }

  return {
    form,
    onSubmit,
  };
};

export default useFormMethods;
