import EmployeeHome from './employee';
import useAuth from '@/lib/hooks/use-auth';
import CompanyList from '../companies/company-list';
import { PageContainer } from '@/lib/components';

const Home = () => {
  const { user } = useAuth();
  return (
    <>
      {user.isSuperAdmin ? (
        <CompanyList />
      ) : user.employeeId ? (
        <EmployeeHome />
      ) : (
        <PageContainer breadCrumb={{ title: 'Home', to: '' }}>
          <h1 className="text-3xl flex justify-center mt-40">Coming soon!!</h1>
        </PageContainer>
      )}
    </>
  );
};

export default Home;
