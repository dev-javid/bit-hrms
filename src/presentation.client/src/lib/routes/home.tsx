import Profile from '@/app/settings';
import { User } from '../types';
import Home from '@/app/home';
import CompanyList from '@/app/companies/company-list';
import CompanyForm from '@/app/companies/company-form';

export default function getHomeRoutes(user: User) {
  const routes = [
    {
      path: '',
      element: !user.isSuperAdmin ? <Home /> : <CompanyList />,
    },
    { path: 'profile', element: <Profile /> },
  ];

  if (user.isSuperAdmin) {
    routes.push({
      path: 'companies/:companyId',
      element: <CompanyForm />,
    });
  }

  return routes;
}
