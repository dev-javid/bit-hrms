import { useForm } from 'react-hook-form';
import { useSignInMutation } from '@/lib/rtk/rtk.auth';
import { zodResolver } from '@hookform/resolvers/zod';
import { useNavigate } from 'react-router-dom';
import FormSchema, { FormSchemaType } from './schema';
import { baseApi } from '@/lib/rtk';
import { useAppDispatch } from '@/lib/store/hooks';

const useSubmitForm = () => {
  const navigateTo = useNavigate();
  const dispatch = useAppDispatch();

  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: {
      email: '',
      password: '',
    },
  });

  const [signIn, { isLoading }] = useSignInMutation();

  async function onSubmit(data: FormSchemaType) {
    const res = await signIn(data);
    if (!('error' in res)) {
      dispatch(baseApi.util.resetApiState());
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
