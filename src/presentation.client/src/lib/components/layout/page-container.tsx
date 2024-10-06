import NavMenuSmall from './nav-menu-small';
import UserMenu from './user-menu';
import HeaderBreadcrumb, { BreadCrumbProps } from './header-breadcrumb';
import useAuth from '@/lib/hooks/use-auth';
import { User } from '@/lib/types';

const getDisplayName = (user: User) => {
  if (user.isSuperAdmin) {
    return 'Super Admin';
  }
  return user.name ?? user.email;
};

const PageContainer = ({ breadCrumb, children }: { children: React.ReactNode; breadCrumb: BreadCrumbProps }) => {
  const { user } = useAuth();
  return (
    <>
      <header className="sticky top-0 z-30 flex h-14 items-center sm:py-4 px-2 border-b bg-background sm:h-auto sm:px-6">
        <NavMenuSmall />
        <HeaderBreadcrumb breadCrumb={breadCrumb} />
        <div className="flex ml-auto ">
          <div className="flex-col px-2 pt-2">
            <div className="text-sm">{getDisplayName(user)}</div>
          </div>
          <div className="relative ml-auto flex-1 grow-0">
            <UserMenu />
          </div>
        </div>
      </header>
      <main className="px-4 sm:py-0">{children}</main>
    </>
  );
};

export default PageContainer;
