import Name from './name';
import useFormMethods from './use-form-methods';
import { DatePicker, Form, FormButtons } from 'xplorer-ui';
import Optional from './optional';
import { startOfYear, endOfYear, addMonths } from 'date-fns';
import { Holiday } from '@/lib/types';
import useDefaultValues from './use-default-values';

const HolidayForm = ({
  holiday,
  onSuccess,
}: {
  holiday?: Holiday;
  onSuccess: () => void;
}) => {
  const defaultValues = useDefaultValues(holiday);
  const { form, onSubmit } = useFormMethods(defaultValues, onSuccess);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
        <Name control={form.control} />
        <DatePicker
          label="Date"
          name="date"
          control={form.control}
          range={(date) => {
            return (
              date >= startOfYear(new Date()) &&
              date <= addMonths(endOfYear(new Date()), 2)
            );
          }}
        />
        <Optional control={form.control} />
        <FormButtons form={form} hideCancel />
      </form>
    </Form>
  );
};

export default HolidayForm;
