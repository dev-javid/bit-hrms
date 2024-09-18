import { Outlet } from 'react-router-dom';
import { NavMenu } from './nav-menu';

const SidebarLayout = () => {
  return (
    <div className="flex min-h-screen w-full flex-col bg-muted/40">
      <NavMenu />
      <div className="flex flex-col pb-2 sm:gap-4 sm:pl-14">
        <Outlet />
      </div>
    </div>
  );
};

export default SidebarLayout;
