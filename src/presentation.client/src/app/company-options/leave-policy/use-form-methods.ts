import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import FormSchema, { FormSchemaType } from './schema';
import { useUpdateLeavePolicyMutation } from '@/lib/rtk/rtk.leave-policy';
import { toast } from 'xplorer-ui';

const useFormMethods = (defaultValues: FormSchemaType) => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: defaultValues,
  });

  const [update] = useUpdateLeavePolicyMutation();

  async function onSubmit(data: FormSchemaType) {
    const response = await update(data);
    if (!('error' in response)) {
      toast({
        variant: 'primary',
        title: 'Success ',
        description: 'Leave policy details saved',
      });
    }
  }

  return {
    form,
    onSubmit,
  };
};

export default useFormMethods;
