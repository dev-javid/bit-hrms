import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import FormSchema, { FormSchemaType } from './schema';
import { useAddDepartmentMutation, useUpdateDepartmentMutation } from '@/lib/rtk/rtk.departments';
import { toast } from 'xplorer-ui';

const useFormMethods = (onSuccess: () => void, defaultValues?: FormSchemaType) => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: defaultValues,
  });

  const [create] = useAddDepartmentMutation();
  const [update] = useUpdateDepartmentMutation();

  async function onSubmit(data: FormSchemaType) {
    const operation = data?.departmentId ? update : create;
    const response = await operation(data);
    if (!('error' in response)) {
      toast({
        variant: 'primary',
        title: 'Success ',
        description: 'Department details saved',
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
