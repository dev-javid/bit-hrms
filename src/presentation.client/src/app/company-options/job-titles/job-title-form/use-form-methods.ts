import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import FormSchema, { FormSchemaType } from './schema';
import { toast } from 'xplorer-ui';
import { useAddJobTitleMutation, useUpdateJobTitleMutation } from '@/lib/rtk/rtk.job-titles';

const useFormMethods = (defaultValues: FormSchemaType, onSuccess: () => void) => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: defaultValues,
  });

  const [create] = useAddJobTitleMutation();
  const [update] = useUpdateJobTitleMutation();

  async function onSubmit(data: FormSchemaType) {
    const operation = data?.jobTitleId ? update : create;
    const response = await operation(data);
    if (!('error' in response)) {
      toast({
        variant: 'primary',
        title: 'Success ',
        description: 'Job title details saved',
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
