import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import FormSchema, { FormSchemaType } from './schema';
import {
  useAddCompanyMutation,
  useUpdateCompanyMutation,
} from '@/lib/rtk/rtk.comapnies';
import { toast } from 'xplorer-ui';

const useFormMethods = (defaultValues: FormSchemaType) => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: defaultValues,
  });

  const [create] = useAddCompanyMutation();
  const [update] = useUpdateCompanyMutation();

  async function onSubmit(data: FormSchemaType) {
    const operation = data?.companyId ? update : create;
    const response = await operation(data);
    if (!('error' in response)) {
      form.reset({
        companyId: 0,
        name: '',
      });

      toast({
        title: 'Success ',
        description: 'Company details saved',
      });
    }
  }

  return {
    form,
    onSubmit,
  };
};

export default useFormMethods;
