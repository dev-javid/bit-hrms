import { FormSchemaType } from './schema';
import useFormMethods from './use-form-methods';
import { Form } from 'xplorer-ui';
import { FormButtons, TextInput } from 'xplorer-ui';

const FormContainer = ({
  defaultValues,
}: {
  defaultValues: FormSchemaType;
}) => {
  const { form, onSubmit } = useFormMethods(defaultValues);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)}>
        <div>
          <div className="my-4">
            <TextInput
              control={form.control}
              label="Casual Leaves (Includes sick leaves)"
              name="casualLeaves"
              placeholder="Casual leaves"
            />
          </div>
          <div className="my-4">
            <TextInput
              control={form.control}
              label="Earned Leaves (Per month)"
              name="earnedLeavesPerMonth"
              placeholder="Earned leaves per month"
            />
          </div>
          <div className="my-4">
            <TextInput
              control={form.control}
              label="Holidays"
              name="holidays"
              placeholder="Holidays"
            />
          </div>
          <FormButtons form={form} hideCancel />
        </div>
      </form>
    </Form>
  );
};

export default FormContainer;
