import useFormMethods from './use-form-methods';
import { Form, FormButtons, TextInput } from 'xplorer-ui';

const IncomeSourceForm = ({ onSuccess }: { onSuccess: () => void }) => {
  const { form, onSubmit } = useFormMethods(onSuccess);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
        <div className="space-y-4">
          <div className="flex flex-col gap-5">
            <TextInput
              type="text"
              control={form.control}
              label="Name"
              name="name"
              placeholder="Name"
            />
            <TextInput
              type="text"
              control={form.control}
              label="Description"
              name="description"
              placeholder="Description"
            />
          </div>
        </div>

        <FormButtons form={form} hideCancel />
      </form>
    </Form>
  );
};

export default IncomeSourceForm;
