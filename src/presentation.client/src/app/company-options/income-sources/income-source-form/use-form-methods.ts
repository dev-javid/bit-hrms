import { useForm } from 'react-hook-form';
import FormSchema, { FormSchemaType } from './schema';
import { zodResolver } from '@hookform/resolvers/zod';
import { useAddIncomeSourceMutation } from '@/lib/rtk/rtk.income-sources';
import { toast } from 'xplorer-ui';

const useFormMethods = (onSuccess: () => void) => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: {
      name: '',
      description: '',
    },
  });

  const [add] = useAddIncomeSourceMutation();

  async function onSubmit(data: FormSchemaType) {
    const response = await add({
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
