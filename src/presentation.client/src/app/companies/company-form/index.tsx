import { Link, useParams } from 'react-router-dom';
import FormContainer from './form-container';
import useDefaultValues from './use-default-values';
import {
  BreadCrumbProps,
  PageContainer,
  PageHeader,
  PageSkeleton,
} from '@/lib/components';
import { Button } from 'xplorer-ui';

export default function CompanyForm() {
  const companyId = useParams().companyId ?? 0;
  const { defaultValues, isLoading } = useDefaultValues();

  const breadCrumb: BreadCrumbProps = {
    title: 'Companies',
    to: './../',
    child: { title: companyId != 0 ? 'Edit' : 'Add', to: '' },
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title={companyId != 0 ? 'Edit Company' : 'Add Company'}>
        <Link to="./../">
          <Button>Company List</Button>
        </Link>
      </PageHeader>
      <PageSkeleton isLoading={isLoading}>
        {defaultValues && <FormContainer defaultValues={defaultValues} />}
      </PageSkeleton>
    </PageContainer>
  );
}
