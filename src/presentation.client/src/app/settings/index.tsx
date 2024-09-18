import { BreadCrumbProps, PageContainer } from '@/lib/components';
import ChangePasswordForm from '../auth/change-password-form';

const breadCrumb: BreadCrumbProps = {
  title: 'Your Profile',
  to: '',
};

const Profile = () => {
  return (
    <PageContainer breadCrumb={breadCrumb}>
      <div className="grid gap-4 sm:grid-cols-2 md:grid-cols-3">
        <ChangePasswordForm />
      </div>
    </PageContainer>
  );
};

export default Profile;
