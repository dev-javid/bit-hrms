import { useForm } from 'react-hook-form';
import FormSchema, { FormSchemaType } from './schema';
import { zodResolver } from '@hookform/resolvers/zod';
import { useAddIncomeSourceMutation } from '@/lib/rtk/rtk.income-sources';

const useFormMethods = (onSuccess: () => void) => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
  });

  const [add] = useAddIncomeSourceMutation();

  async function onSubmit(data: FormSchemaType) {
    const response = await add({
      ...data,
    });

    if (!('error' in response)) {
      onSuccess();
    }
  }

  return {
    form,
    onSubmit,
  };
};

export default useFormMethods;
