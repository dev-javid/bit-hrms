import { useEffect } from 'react';
import useFormMethods from './use-form-methods';
import { Form } from 'xplorer-ui';
import { FormButtons, TextInput } from 'xplorer-ui';
import { EmployeeLeave } from '@/lib/types';

const FormContainer = ({ leave, onCancelClick }: { leave: EmployeeLeave; onCancelClick: () => void }) => {
  const { form, onSubmit } = useFormMethods(leave, onCancelClick);
  useEffect(() => {
    form.reset({
      remarks: '',
    });
  }, [leave, form]);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
        <TextInput name="remarks" label="Please enter remarks/reason for declining the leave" placeholder="Remarks" control={form.control} />
        <FormButtons form={form} onCancel={onCancelClick} />
      </form>
    </Form>
  );
};

export default FormContainer;
