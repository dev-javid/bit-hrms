import {
  BreadCrumbProps,
  PageContainer,
  PageHeader,
  PageSkeleton,
} from '@/lib/components';
import FormContainer from './form-container';
import useDefaultValues from './use-default-values';
import { CardContent, Card, Button } from 'xplorer-ui';
import { Link } from 'react-router-dom';

export default function LeavePolicyForm() {
  const { defaultValues, isLoading } = useDefaultValues();

  const breadCrumb: BreadCrumbProps = {
    title: 'Administration',
    to: './../',
    child: { title: 'Leave Policy', to: '' },
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title="Leave Policy">
        <Link to="./../holidays">
          <Button variant="link">Holidays</Button>
        </Link>
      </PageHeader>
      <PageSkeleton isLoading={isLoading} form>
        <Card>
          <CardContent>
            <div className="w-1/4">
              <FormContainer defaultValues={defaultValues} />
            </div>
          </CardContent>
        </Card>
      </PageSkeleton>
    </PageContainer>
  );
}
