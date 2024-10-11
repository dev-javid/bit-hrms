import { PageSkeleton } from '@/lib/components';
import useFormMethods from './use-form-methods';
import { Form } from 'xplorer-ui';
import { FormButtons, TextInput } from 'xplorer-ui';
import useDefaultValues from './use-default-values';

const LeavePolicyForm = () => {
  const { defaultValues, isLoading } = useDefaultValues();
  const { form, onSubmit } = useFormMethods(defaultValues);

  return (
    <PageSkeleton isLoading={isLoading}>
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <div>
            <TextInput control={form.control} label="Casual Leaves (Includes sick leaves)" name="casualLeaves" placeholder="Casual leaves" />
            <TextInput
              className="mt-4"
              control={form.control}
              label="Earned Leaves (Per month)"
              name="earnedLeavesPerMonth"
              placeholder="Earned leaves per month"
            />
            <TextInput className="mt-4 mb-4" control={form.control} label="Holidays" name="holidays" placeholder="Holidays" />
            <FormButtons form={form} hideCancel />
          </div>
        </form>
      </Form>
    </PageSkeleton>
  );
};

export default LeavePolicyForm;
