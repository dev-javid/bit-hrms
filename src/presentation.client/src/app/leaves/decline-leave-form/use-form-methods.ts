import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import FormSchema, { FormSchemaType } from './schema';
import { toast } from 'xplorer-ui';
import { EmployeeLeave } from '@/lib/types';
import { useDeclineEamployeeLeaveMutation } from '@/lib/rtk/rtk.employee-leaves';

const useFormMethods = (leave: EmployeeLeave, onSuccess: () => void) => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(FormSchema),
    defaultValues: {
      remarks: '',
    },
  });

  const [decline] = useDeclineEamployeeLeaveMutation();

  async function onSubmit(data: FormSchemaType) {
    const response = await decline({
      ...data,
      employeeLeaveId: leave.employeeLeaveId,
    });

    if (!('error' in response)) {
      toast({
        variant: 'primary',
        title: 'Success ',
        description: 'Leave declined',
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
