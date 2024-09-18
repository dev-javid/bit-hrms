import { useForm } from 'react-hook-form';
import { useForgotPasswordMutation } from '@/lib/rtk/rtk.auth';
import { zodResolver } from '@hookform/resolvers/zod';
import FormSchema, { FormSchemaType } from './schema';
import { toast } from 'xplorer-ui';

const useSubmitForm = () => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: {
      email: '',
    },
  });

  const [forgot, { isLoading }] = useForgotPasswordMutation();

  async function onSubmit(data: FormSchemaType) {
    const res = await forgot(data);
    if (!('error' in res)) {
      toast({
        variant: 'primary',
        title: 'Email Sent',
        description: 'Check your email for further instructions',
      });
    }
  }

  return {
    form,
    onSubmit,
    isLoading,
  };
};

export default useSubmitForm;
