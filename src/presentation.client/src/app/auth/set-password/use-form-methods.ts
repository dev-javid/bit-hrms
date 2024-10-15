import { useForm } from 'react-hook-form';
import { useResetPasswordMutation, useSetPasswordMutation } from '@/lib/rtk/rtk.auth';
import { zodResolver } from '@hookform/resolvers/zod';
import { useNavigate } from 'react-router-dom';
import FormSchema, { FormSchemaType } from './schema';
import { toast } from 'xplorer-ui';

const useSubmitForm = (defaultValues: FormSchemaType, reset: boolean) => {
  const navigateTo = useNavigate();

  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: defaultValues,
  });

  const [setPassword, { isLoading }] = useSetPasswordMutation();
  const [resetPassword] = useResetPasswordMutation();

  async function onSubmit(data: FormSchemaType) {
    const res = await (!reset ? setPassword(data) : resetPassword(data));
    if (!('error' in res)) {
      toast({
        variant: 'primary',
        title: 'Success ',
        description: 'Password set successfully, you can now sign-in to your account.',
      });
      navigateTo('/app');
    }
  }

  return {
    form,
    onSubmit,
    isLoading,
  };
};

export default useSubmitForm;
