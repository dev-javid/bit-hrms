import MonthlyAttendance from '@/app/attendance';
import RegularizationList from '@/app/attendance/regularization/regularization-list';

const routes = [
  {
    path: 'attendance',
    element: <MonthlyAttendance />,
  },
  {
    path: 'attendance/regularizations',
    element: <RegularizationList />,
  },
];

export default routes;
