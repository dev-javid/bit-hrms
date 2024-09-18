import { Navigate, Outlet } from 'react-router-dom';
import SignIn from '@/app/auth/sign-in';
import { SidebarLayout } from '@/lib/components';
import companyOptions from './company-options';
import employees from './employees';
import leaves from './leaves';
import { compensationRoutes, salaryRoutes } from './financials';
import attendance from './attendance';
import SetPassword from '@/app/auth/set-password';
import ForgotPassword from '@/app/auth/forgot-password';
import { User } from '../types';
import getHomeRoutes from './home';

const routes = (isLoggedIn: boolean, user: User) => [
  {
    path: '/app',
    element: isLoggedIn ? <SidebarLayout /> : <Navigate to="/auth/sign-in" />,
    children: [
      ...getHomeRoutes(user),
      ...companyOptions,
      ...employees,
      ...leaves,
      ...attendance,
      ...compensationRoutes,
      ...salaryRoutes,
    ],
  },
  {
    path: '/',
    element: !isLoggedIn ? <Outlet /> : <Navigate to="/app" />,
    children: [
      { path: 'sign-in', element: <SignIn /> },
      { path: 'set-password/:userId', element: <SetPassword reset={false} /> },
      {
        path: 'reset-password/:userId',
        element: <SetPassword reset={true} />,
      },
      { path: 'forgot-password', element: <ForgotPassword /> },
      { path: '', element: <SignIn /> },
      { path: '*', element: <SignIn /> },
    ],
  },
];

export default routes;
