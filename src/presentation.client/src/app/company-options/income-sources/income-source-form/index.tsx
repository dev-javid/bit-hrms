import { IncomeSource } from '@/lib/types';
import useFormMethods from './use-form-methods';
import { Form, FormButtons, TextInput } from 'xplorer-ui';

const IncomeSourceForm = ({ incomeSource, onSuccess }: { incomeSource?: IncomeSource; onSuccess: () => void }) => {
  const { form, onSubmit } = useFormMethods(
    {
      incomeSourceId: incomeSource?.incomeSourceId || 0,
      name: incomeSource?.name || '',
      description: incomeSource?.description || '',
    },
    onSuccess,
  );

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
        <div className="space-y-4">
          <div className="flex flex-col gap-5">
            <TextInput type="text" control={form.control} label="Name" name="name" placeholder="Name" />
            <TextInput type="text" control={form.control} label="Description" name="description" placeholder="Description" />
          </div>
        </div>

        <FormButtons form={form} hideCancel loading={form.formState.isSubmitting} />
      </form>
    </Form>
  );
};

export default IncomeSourceForm;
