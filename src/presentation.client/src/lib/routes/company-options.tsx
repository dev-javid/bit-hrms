import CompanyOptions from '@/app/company-options';
import DeductionTypesList from '@/app/company-options/deduction-types/deduction-types-list';
import DepartmentList from '@/app/company-options/departments/department-list';
import HolidayList from '@/app/company-options/holidays/holiday-list';
import IncomeSourceList from '@/app/company-options/income-sources/income-source-list';
import JobTitleList from '@/app/company-options/job-titles/job-title-list';
import LeavePolicyForm from '@/app/company-options/leave-policy';
import SalaryList from '@/app/salary/salary-list';

const routes = [
  {
    path: 'company-options',
    element: <CompanyOptions />,
  },
  {
    path: 'company-options/holidays',
    element: <HolidayList />,
  },
  {
    path: 'company-options/leave-policy',
    element: <LeavePolicyForm />,
  },
  {
    path: 'company-options/departments',
    element: <DepartmentList />,
  },
  {
    path: 'company-options/job-titles',
    element: <JobTitleList />,
  },
  {
    path: 'company-options/deduction-types',
    element: <DeductionTypesList />,
  },
  {
    path: 'company-options/salary-management',
    element: <SalaryList />,
  },
  {
    path: 'company-options/income-sources',
    element: <IncomeSourceList />,
  },
];

export default routes;
