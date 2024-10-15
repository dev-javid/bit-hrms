import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import FormSchema, { FormSchemaType } from './schema';
import { useAddRegularizationMutation } from '@/lib/rtk/rtk.attendance';

const useFormMethods = (defaultValues: FormSchemaType, onSuccess: () => void) => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: defaultValues,
  });

  const [add, { isLoading }] = useAddRegularizationMutation();

  async function onSubmit(data: FormSchemaType) {
    const response = await add({
      ...data,
      clockInTime: data.clockInTime + ':00',
      clockOutTime: data.clockOutTime + ':00',
    });

    if (!('error' in response)) {
      onSuccess();
    }
  }

  return {
    form,
    onSubmit,
    isLoading,
  };
};

export default useFormMethods;
