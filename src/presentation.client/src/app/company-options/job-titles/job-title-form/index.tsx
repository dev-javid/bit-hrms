import { PageSkeleton } from '@/lib/components';
import FormContainer from './form-container';
import useDefaultValues from './use-default-values';
import { JobTitle } from '@/lib/types';

export default function JobTitleForm({
  jobTitle,
  onSuccess,
}: {
  jobTitle?: JobTitle;
  onSuccess: () => void;
}) {
  const { defaultValues, departments, isLoading } = useDefaultValues(jobTitle);

  return (
    <PageSkeleton isLoading={isLoading}>
      {defaultValues && (
        <FormContainer
          onSuccess={onSuccess}
          defaultValues={defaultValues}
          departments={departments}
        />
      )}
    </PageSkeleton>
  );
}
