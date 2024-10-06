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
            <div className="my-4">
              <TextInput control={form.control} label="Casual Leaves (Includes sick leaves)" name="casualLeaves" placeholder="Casual leaves" />
            </div>
            <div className="my-4">
              <TextInput control={form.control} label="Earned Leaves (Per month)" name="earnedLeavesPerMonth" placeholder="Earned leaves per month" />
            </div>
            <div className="my-4">
              <TextInput control={form.control} label="Holidays" name="holidays" placeholder="Holidays" />
            </div>
            <FormButtons form={form} hideCancel />
          </div>
        </form>
      </Form>
    </PageSkeleton>
  );
};

export default LeavePolicyForm;
