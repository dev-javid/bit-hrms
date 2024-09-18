import { PageContainer } from '@/lib/components';
import Profile from './profile';
import NextSaturday from './next-saturday';
import LeaveSummary from './leave-summary';
import Holidays from './holidays';
import ClockInOut from '@/app/attendance/clock-in-out';

const EmployeeHome = () => {
  return (
    <PageContainer
      breadCrumb={{
        title: 'Home',
        to: '',
      }}
    >
      <div className="grid gap-4 lg:grid-cols-2">
        <div className="space-y-4">
          <Profile />
          <ClockInOut />
        </div>

        <div className="space-y-4">
          <NextSaturday />
          <LeaveSummary hideActions={false} />
          <Holidays />
        </div>
      </div>
    </PageContainer>
  );
};

export default EmployeeHome;
