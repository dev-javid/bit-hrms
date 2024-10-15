import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import FormSchema, { FormSchemaType } from './schema';
import { useAddCompensationMutation } from '@/lib/rtk/rtk.salary';
import { toast } from 'xplorer-ui';
import { DateOnly, Employee } from '@/lib/types';

const useFormMethods = (employee: Employee, onSuccess: () => void) => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: {
      employeeId: employee.employeeId,
    },
  });

  const [addCompensation] = useAddCompensationMutation();

  async function onSubmit(data: FormSchemaType) {
    const response = await addCompensation({
      ...data,
      effectiveFrom: DateOnly.fromDate(data.effectiveFrom).toDateOnlyISOString(),
    });

    if (!('error' in response)) {
      onSuccess();
      toast({
        variant: 'primary',
        title: 'Success ',
        description: 'Compensation details saved',
      });
    }
  }
  return {
    form,
    onSubmit,
  };
};

export default useFormMethods;
