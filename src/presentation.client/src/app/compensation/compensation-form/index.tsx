import { Employee } from '@/lib/types';
import useFormMethods from './use-form-methods';
import { Form } from 'xplorer-ui';
import { DatePicker, FormButtons, TextInput } from 'xplorer-ui';

const CompensationForm = ({ employee, onSuccess }: { employee: Employee; onSuccess: () => void }) => {
  const { form, onSubmit } = useFormMethods(employee, onSuccess);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)}>
        <div className="pb-4">
          <TextInput control={form.control} name="amount" label="Amount" placeholder="Amount" type="number" />
          <DatePicker
            label="Effective From"
            name="effectiveFrom"
            control={form.control}
            range={(date) => {
              return date >= new Date();
            }}
            placeHolder="Effective From"
          />
        </div>
        <FormButtons form={form} hideCancel />
      </form>
    </Form>
  );
};

export default CompensationForm;
