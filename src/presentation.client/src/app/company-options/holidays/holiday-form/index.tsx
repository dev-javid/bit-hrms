import useFormMethods from './use-form-methods';
import { Alert, AlertDescription, DatePicker, Form, FormButtons, SwitchInput, TextInput } from 'xplorer-ui';
import { Holiday, LeavePolicy } from '@/lib/types';
import useDefaultValues from './use-default-values';
import { startOfYear, addMonths, endOfYear } from 'date-fns';
import { AlertCircle } from 'lucide-react';
import { useCallback, useEffect, useState } from 'react';

const HolidayForm = ({
  holiday,
  leavePolicy,
  existingHolidaysCount,
  onSuccess,
}: {
  holiday?: Holiday;
  leavePolicy?: LeavePolicy;
  existingHolidaysCount: number;
  onSuccess: () => void;
}) => {
  const [error, setError] = useState({ leavePolicy: '', exceeded: '' });
  const defaultValues = useDefaultValues(holiday);
  const { form, onSubmit } = useFormMethods(defaultValues, onSuccess);

  const checkIfExceeded = useCallback(
    (optional: boolean | undefined) => {
      if (!holiday && leavePolicy && !optional) {
        if (existingHolidaysCount >= leavePolicy.holidays) {
          setError((x) => ({
            ...x,
            exceeded: `Maximum number of holidays reached. As per your leave policy you can have only ${leavePolicy.holidays} holidays. You can add optional holiday though.`,
          }));
        }
      } else {
        setError((x) => ({ ...x, exceeded: '' }));
      }
    },
    [leavePolicy, existingHolidaysCount, holiday]
  );

  useEffect(() => {
    if (!leavePolicy) {
      setError((x) => ({
        ...x,
        leavePolicy: 'Leave policy is required before you start adding/updating holidays. Please configure leave policy first.',
      }));
    }
  }, [leavePolicy]);

  useEffect(() => checkIfExceeded(undefined), [checkIfExceeded]);
  form.watch(({ optional }) => checkIfExceeded(optional));

  return (
    <>
      {error.leavePolicy && (
        <Alert variant="destructive" className="mb-4">
          <AlertCircle className="h-4 w-4" />
          <AlertDescription>{error.leavePolicy}</AlertDescription>
        </Alert>
      )}
      {error.exceeded && (
        <Alert variant="destructive" className="mb-4">
          <AlertCircle className="h-4 w-4" />
          <AlertDescription>{error.exceeded}</AlertDescription>
        </Alert>
      )}

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
          <SwitchInput
            name="optional"
            control={form.control}
            label="Optional Holiday"
            description="Employees can take only one holiday from the optional holidays."
          />
          <FormButtons form={form} hideCancel loading={form.formState.isSubmitting} />
        </form>
      </Form>
    </>
  );
};

export default HolidayForm;
