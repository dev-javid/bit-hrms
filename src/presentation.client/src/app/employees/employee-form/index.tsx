import FormContainer from './form-container';
import useDefaultValues from './use-default-values';
import { BackButton, BreadCrumbProps, PageContainer, PageHeader } from '@/lib/components';
import { Container } from 'xplorer-ui';

export default function EmployeeForm() {
  const { defaultValues, departments, isLoading, fullName } = useDefaultValues();

  const breadCrumb: BreadCrumbProps = {
    title: 'Employees',
    to: './../../',
    child: {
      title: fullName || 'Add',
      to: './../',
    },
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title={defaultValues.employeeId != 0 ? 'Edit Employee' : 'Add Employee'}>
        <BackButton to="./../" text="Employee List" />
      </PageHeader>
      <Container isLoading={isLoading}>
        <>{defaultValues && <FormContainer defaultValues={defaultValues} departments={departments} />}</>
      </Container>
    </PageContainer>
  );
}
