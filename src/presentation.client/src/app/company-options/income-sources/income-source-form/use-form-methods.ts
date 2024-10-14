import { useForm } from 'react-hook-form';
import FormSchema, { FormSchemaType } from './schema';
import { zodResolver } from '@hookform/resolvers/zod';
import { useAddIncomeSourceMutation, useUpdateIncomeSourceMutation } from '@/lib/rtk/rtk.income-sources';
import { toast } from 'xplorer-ui';

const useFormMethods = (defaultValues: FormSchemaType, onSuccess: () => void) => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: {
      ...defaultValues,
    },
  });

  const [add] = useAddIncomeSourceMutation();
  const [update] = useUpdateIncomeSourceMutation();

  async function onSubmit(data: FormSchemaType) {
    const func = defaultValues.incomeSourceId ? update : add;
    const response = await func({
      ...data,
    });

    if (!('error' in response)) {
      toast({
        variant: 'primary',
        title: 'Success ',
        description: 'Income source saved',
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
