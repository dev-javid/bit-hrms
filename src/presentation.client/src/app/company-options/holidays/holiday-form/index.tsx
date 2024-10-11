import useFormMethods from './use-form-methods';
import { DatePicker, Form, FormButtons, TextInput } from 'xplorer-ui';
import Optional from './optional';
import { Holiday } from '@/lib/types';
import useDefaultValues from './use-default-values';
import { startOfYear, addMonths, endOfYear } from 'date-fns';

const HolidayForm = ({ holiday, onSuccess }: { holiday?: Holiday; onSuccess: () => void }) => {
  const defaultValues = useDefaultValues(holiday);
  const { form, onSubmit } = useFormMethods(defaultValues, onSuccess);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
        <TextInput name="name" label="Name" placeholder="Holiday Name" control={form.control} />
        <DatePicker
          placeHolder="Holiday Date"
          label="Date"
          name="date"
          control={form.control}
          range={(date) => {
            return date >= startOfYear(new Date()) && date <= addMonths(endOfYear(new Date()), 2);
          }}
        />

        <Optional control={form.control} />
        <FormButtons form={form} hideCancel loading={form.formState.isSubmitting} />
      </form>
    </Form>
  );
};

export default HolidayForm;
