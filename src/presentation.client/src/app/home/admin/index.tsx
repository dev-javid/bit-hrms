import { BreadCrumbProps, PageContainer } from '@/lib/components';
import AdministrationLinks from './administration-links';
import NextSaturday from '../employee/next-saturday';

const breadCrumb: BreadCrumbProps = {
  title: 'Home',
  to: '',
};

const AdminHome = () => {
  return (
    <PageContainer breadCrumb={breadCrumb}>
      <div className="grid gap-4 sm:grid-cols-2 md:grid-cols-6 xl:grid-cols-4">
        <AdministrationLinks />
        <div className='lg:col-span-2'>
          <NextSaturday />
        </div>
      </div>
    </PageContainer>
  );
};

export default AdminHome;
