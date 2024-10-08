import useFormMethods from './use-form-methods';
import { Form, FormButtons, TextInput } from 'xplorer-ui';
import useDefaultValues from './use-default-values';
import { Department } from '@/lib/types';

const DepartmentForm = ({ department, onSuccess }: { department?: Department; onSuccess: () => void }) => {
  const defaultValues = useDefaultValues(department);
  const { form, onSubmit } = useFormMethods(onSuccess, defaultValues);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className=" space-y-4">
        <TextInput name="name" label="Name" placeholder="Department Name" control={form.control} />
        <FormButtons form={form} hideCancel loading={form.formState.isSubmitting} />
      </form>
    </Form>
  );
};

export default DepartmentForm;
