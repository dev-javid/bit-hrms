import { FormSchemaType } from './schema';
import useFormMethods from './use-form-methods';
import { Form, FormButtons, TextInput, TimePicker } from 'xplorer-ui';

const FormContainer = ({ defaultValues, onSuccess }: { defaultValues: FormSchemaType; onSuccess: () => void }) => {
  const { form, onSubmit, isLoading } = useFormMethods(defaultValues, onSuccess);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
        <div className="space-y-4">
          <div className="flex gap-5">
            <TimePicker
              disabled={!!defaultValues.clockInTime}
              control={form.control}
              label="Clock In Time"
              name="clockInTime"
              placeholder="Clock In Time"
            />
            <TimePicker control={form.control} label="Clock Out Time" name="clockOutTime" placeholder="Clock Out Time" />
          </div>
          <TextInput control={form.control} label="Reason" name="reason" placeholder="Reason" />
        </div>

        <FormButtons form={form} hideCancel loading={isLoading} />
      </form>
    </Form>
  );
};

export default FormContainer;
