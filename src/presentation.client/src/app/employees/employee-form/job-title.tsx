import { UseFormReturn } from 'react-hook-form';
import { FormSchemaType } from './schema';
import { useGetJobTitlesQuery } from '@/lib/rtk/rtk.job-titles';
import { SimpleSelect } from 'xplorer-ui';

const JobTitle = ({ form, defaultValue }: { form: UseFormReturn<FormSchemaType>; defaultValue: number }) => {
  const departmentId = form.watch('departmentId');

  const { data, isLoading, isFetching } = useGetJobTitlesQuery({
    departmentId,
  });

  return (
    <SimpleSelect
      control={form.control}
      label="Job Title"
      name="jobTitleId"
      defaultValue={defaultValue?.toString()}
      disabled={isLoading || isFetching}
      options={(data?.items ?? []).map((x) => ({
        label: x.name,
        value: x.jobTitleId.toString(),
      }))}
      placeholder="Select Job Title"
    />
  );
};

export default JobTitle;
