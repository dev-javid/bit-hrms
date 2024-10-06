import { useParams } from 'react-router-dom';
import FormContainer from './form-container';
import useDefaultValues from './use-default-values';
import { BackButton, BreadCrumbProps, PageContainer, PageHeader } from '@/lib/components';

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
        <BackButton to="./../" text="Employee List" />
      </PageHeader>
      <>{defaultValues && <FormContainer defaultValues={defaultValues} departments={departments} />}</>
    </PageContainer>
  );
}
