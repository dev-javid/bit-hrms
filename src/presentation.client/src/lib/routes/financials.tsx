import CompensationList from '@/app/compensation/compensation-list';
import EstimatedSalary from '@/app/salary/estimated-salary';
import SalaryList from '@/app/salary/salary-list';

export const salaryRoutes = [
  {
    path: 'salary',
    element: <SalaryList />,
  },
  {
    path: 'salary/estimated-salary',
    element: <EstimatedSalary />,
  },
];

export const compensationRoutes = [
  {
    path: 'compensation',
    element: <CompensationList />,
  },
];
