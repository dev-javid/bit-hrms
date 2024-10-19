import useFormMethods from './use-form-methods';
import { Alert, AlertDescription, Container, DatePicker, Form, FormButtons, SwitchInput, TextInput } from 'xplorer-ui';
import { Holiday } from '@/lib/types';
import useDefaultValues from './use-default-values';
import { startOfYear, addMonths, endOfYear } from 'date-fns';
import { AlertCircle } from 'lucide-react';
import { useCallback, useEffect, useState } from 'react';

const HolidayForm = ({ holiday, onSuccess }: { holiday?: Holiday; onSuccess: () => void }) => {
  const [error, setError] = useState({ leavePolicy: '', exceeded: '' });
  const { formDefaultValues, isLoading, leavePolicy, currentUtilizableHolidays } = useDefaultValues(holiday);
  const { form, onSubmit } = useFormMethods(formDefaultValues, onSuccess);

  const checkIfExceeded = useCallback(
    (optional: boolean | undefined) => {
      if (leavePolicy && !optional) {
        if (!holiday) {
          if (currentUtilizableHolidays >= leavePolicy.holidays) {
            setError((x) => ({
              ...x,
              exceeded: `Maximum holiday limit reached. According to your leave policy, you are allowed a total of ${leavePolicy.holidays} holidays. However, you can still add optional holidays.`,
            }));
          }
        } else {
          if (holiday.optional && currentUtilizableHolidays >= leavePolicy.holidays) {
            setError((x) => ({
              ...x,
              exceeded: `Maximum holiday limit reached. According to your leave policy, you are allowed a total of ${leavePolicy.holidays} holidays. However, you can still add optional holidays.`,
            }));
          }
        }
      } else {
        setError((x) => ({ ...x, exceeded: '' }));
      }
    },
    [leavePolicy, currentUtilizableHolidays, holiday]
  );

  useEffect(() => {
    if (!isLoading && !leavePolicy) {
      setError((x) => ({
        ...x,
        leavePolicy: 'Leave policy is required before you start adding/updating holidays. Please configure leave policy first.',
      }));
    } else {
      setError((x) => ({ ...x, leavePolicy: '' }));
    }
  }, [leavePolicy, isLoading]);

  useEffect(() => {
    if (!holiday) {
      checkIfExceeded(undefined);
    }
  }, [checkIfExceeded, holiday, leavePolicy]);

  form.watch(({ optional }) => checkIfExceeded(optional));

  return (
    <Container isLoading={isLoading}>
      {error.leavePolicy && (
        <Alert className="mb-4 bg-destructive">
          <AlertCircle className="h-4 w-4" />
          <AlertDescription>{error.leavePolicy}</AlertDescription>
        </Alert>
      )}
      {error.exceeded && (
        <Alert className="mb-4 bg-destructive">
          <AlertCircle className="h-4 w-4" />
          <AlertDescription>{error.exceeded}</AlertDescription>
        </Alert>
      )}

      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
          <TextInput name="name" label="Name" placeholder="Holiday Name" control={form.control} />
          <SwitchInput
            name="optional"
            control={form.control}
            label="Optional Holiday"
            description="Employees can take only one holiday from the optional holidays."
          />
          <DatePicker
            placeholder="Holiday Date"
            label="Holiday Date"
            name="date"
            control={form.control}
            minDate={startOfYear(new Date())}
            maxDate={addMonths(endOfYear(new Date()), 2)}
          />
          <FormButtons form={form} hideCancel loading={form.formState.isSubmitting} />
        </form>
      </Form>
    </Container>
  );
};

export default HolidayForm;
