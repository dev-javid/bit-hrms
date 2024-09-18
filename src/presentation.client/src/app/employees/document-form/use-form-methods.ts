import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { toast } from 'xplorer-ui';
import { useNavigate } from 'react-router-dom';
import { useSetEmployeeDocumentMutation } from '@/lib/rtk/rtk.employees';
import { FormSchema, FormSchemaType } from './schema';

const useFormMethods = (defaultValues: FormSchemaType) => {
  const navigateTo = useNavigate();
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: defaultValues,
  });

  const [set] = useSetEmployeeDocumentMutation();

  async function onSubmit(data: FormSchemaType) {
    const response = await set(data);
    if (!('error' in response)) {
      toast({
        variant: 'primary',
        title: 'Success ',
        description: 'Employee details saved',
      });
      navigateTo('/app/employees');
    }
  }

  return {
    form,
    onSubmit,
  };
};

export default useFormMethods;
