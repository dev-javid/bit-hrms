import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { toast } from 'xplorer-ui';
import { useSetEmployeeDocumentMutation } from '@/lib/rtk/rtk.employees';
import { FormSchema, FormSchemaType } from './schema';

const useFormMethods = (defaultValues: FormSchemaType, onSuccess: () => void) => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: defaultValues,
  });

  const [set] = useSetEmployeeDocumentMutation();

  async function onSubmit(data: FormSchemaType) {
    const response = await set(data);
    if (!('error' in response)) {
      onSuccess();
      toast({
        variant: 'primary',
        title: 'Success ',
        description: 'Employee details saved',
      });
    }
  }

  return {
    form,
    onSubmit,
  };
};

export default useFormMethods;
