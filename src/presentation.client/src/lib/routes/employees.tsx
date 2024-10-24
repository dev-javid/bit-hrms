import DocumentsList from '@/app/employees/documents-list';
import EmployeeForm from '@/app/employees/employee-form';
import EmployeesList from '@/app/employees/employee-list';

const routes = [
  {
    path: 'employees',
    element: <EmployeesList />,
  },
  {
    path: 'employees/:employeeId',
    element: <EmployeeForm />,
  },
  {
    path: 'employees/:employeeId/documents',
    element: <DocumentsList />,
  },
];

export default routes;
