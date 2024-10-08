import { PageSkeleton } from '@/lib/components';
import useDefaultValues from './use-default-values';
import { JobTitle } from '@/lib/types';
import { TextInput, SimpleSelect, FormButtons } from 'xplorer-ui';
import useFormMethods from './use-form-methods';
import { Form } from 'xplorer-ui';

export default function JobTitleForm({ jobTitle, onSuccess }: { jobTitle?: JobTitle; onSuccess: () => void }) {
  const { defaultValues, departments, isLoading } = useDefaultValues(jobTitle);
  const { form, onSubmit } = useFormMethods(defaultValues, onSuccess);

  return (
    <PageSkeleton isLoading={isLoading}>
      {defaultValues && (
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <div>
              <TextInput control={form.control} label="Name" name="name" placeholder="Job title name" />
            </div>
            <div className="my-4">
              <SimpleSelect
                control={form.control}
                label="Department"
                name="departmentId"
                options={departments.map((x) => ({
                  label: x.name,
                  value: x.departmentId.toString(),
                }))}
                defaultValue={defaultValues.departmentId ? defaultValues.departmentId?.toString() : ''}
                disabled={defaultValues.jobTitleId !== 0}
              />
            </div>
            <FormButtons form={form} hideCancel loading={form.formState.isSubmitting} />
          </form>
        </Form>
      )}
    </PageSkeleton>
  );
}
