import EmployeeForm from '@/app/employees/employee-form';
import EmployeesList from '@/app/employees/employee-list';
import EmployeeLeaveList from '@/app/leaves/leave-list';

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
    path: 'employees/leaves',
    element: <EmployeeLeaveList />,
  },
];

export default routes;
