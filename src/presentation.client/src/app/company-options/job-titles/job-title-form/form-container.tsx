import { FormSchemaType } from './schema';
import useFormMethods from './use-form-methods';
import { Form } from 'xplorer-ui';
import { FormButtons, SimpleSelect, TextInput } from 'xplorer-ui';
import { Department } from '@/lib/types';

const FormContainer = ({
  defaultValues,
  departments,
  onSuccess,
}: {
  defaultValues: FormSchemaType;
  departments: Department[];
  onSuccess: () => void;
}) => {
  const { form, onSubmit } = useFormMethods(defaultValues, onSuccess);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)}>
        <div>
          <TextInput
            control={form.control}
            label="Name"
            name="name"
            placeholder="Job title name"
          />
        </div>
        <div className="my-4">
          <SimpleSelect
            control={form.control}
            label="Department"
            name="departmentId"
            options={departments.map((x) => ({
              label: x.name,
              value: x.departmentId.toString(),
            }))}
            defaultValue="Department"
          />
        </div>
        <FormButtons form={form} hideCancel />
      </form>
    </Form>
  );
};

export default FormContainer;
