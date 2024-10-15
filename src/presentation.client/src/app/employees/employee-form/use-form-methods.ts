import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { toast } from 'xplorer-ui';
import { useNavigate } from 'react-router-dom';
import { useAddEmployeeMutation, useUpdateEmployeeMutation } from '@/lib/rtk/rtk.employees';
import { FormSchema, FormSchemaType } from './schema';
import { DateOnly } from '@/lib/types';

const useFormMethods = (defaultValues: FormSchemaType) => {
  const navigateTo = useNavigate();
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: defaultValues,
  });

  const [create] = useAddEmployeeMutation();
  const [update] = useUpdateEmployeeMutation();

  async function onSubmit(data: FormSchemaType) {
    const operation = data?.employeeId ? update : create;
    const response = await operation({
      ...data,
      dateOfBirth: DateOnly.fromDate(data.dateOfBirth).toDateOnlyISOString(),
      dateOfJoining: DateOnly.fromDate(data.dateOfJoining).toDateOnlyISOString(),
    });

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
