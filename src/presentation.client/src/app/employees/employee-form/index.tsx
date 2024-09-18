import { Link, useParams } from 'react-router-dom';
import FormContainer from './form-container';
import useDefaultValues from './use-default-values';
import { BreadCrumbProps, PageContainer, PageHeader } from '@/lib/components';
import { Button } from 'xplorer-ui';

export default function EmployeeForm() {
  const employeeId = useParams().employeeId ?? 0;
  const { defaultValues, departments } = useDefaultValues();

  const breadCrumb: BreadCrumbProps = {
    title: 'Employees',
    to: './../',
    child: { title: employeeId != 0 ? 'Edit' : 'Add', to: '' },
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title={employeeId != 0 ? 'Edit Employee' : 'Add Employee'}>
        <Link to="./../">
          <Button variant="link">Employee List</Button>
        </Link>
      </PageHeader>
      <>
        {defaultValues && (
          <FormContainer
            defaultValues={defaultValues}
            departments={departments}
          />
        )}
      </>
    </PageContainer>
  );
}
