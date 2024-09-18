import EmployeeHome from './employee';
import AdminHome from './admin';
import useAuth from '@/lib/hooks/use-auth';

const Home = () => {
  const { user } = useAuth();
  return <>{user.isSuperAdmin ? <AdminHome /> : <EmployeeHome />}</>;
};

export default Home;
