import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import FormSchema, { FormSchemaType } from './schema';
import { useUpdatePasswordMutation } from '@/lib/rtk/rtk.auth';
import { toast } from 'xplorer-ui';

const useFormMethods = () => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: {
      currentPassword: '',
      newPassword: '',
    },
  });

  const [update] = useUpdatePasswordMutation();

  async function onSubmit(data: FormSchemaType) {
    const response = await update(data);
    if (!('error' in response)) {
      toast({
        variant: 'primary',
        title: 'Success ',
        description: 'Password changed',
      });
    }

    return {
      form,
      onSubmit,
    };
  }

  return { onSubmit, form };
};

export default useFormMethods;
