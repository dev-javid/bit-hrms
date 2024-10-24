import { RoleName } from '@/lib/types';
import useAuth from '@/lib/hooks/use-auth';
import { Home, IndianRupee, MonitorOff, Users2, CalendarDays, GripIcon } from 'lucide-react';
import { Link, useLocation } from 'react-router-dom';
import { Tooltip, TooltipTrigger, TooltipContent, cn, ThemeToggle } from 'xplorer-ui';
import _ from 'lodash';

const navMenuItems: {
  title: string;
  link: string;
  icon: React.ReactElement;
  authorized: RoleName[];
  isActive: (pathName: string) => boolean;
}[] = [
  {
    title: 'Home',
    link: '/app',
    icon: <Home className="h-5 w-5" />,
    authorized: ['SuperAdmin', 'CompanyAdmin', 'Employee'],
    isActive: (pathName) => pathName == '/app',
  },
  {
    title: 'Employees',
    link: '/app/employees',
    icon: <Users2 className="h-5 w-5" />,
    authorized: ['CompanyAdmin'],
    isActive: (pathName) => pathName.includes('/app/employees'),
  },
  {
    title: 'Attendance',
    link: '/app/attendance',
    icon: <CalendarDays className="h-5 w-5" />,
    authorized: ['Employee', 'CompanyAdmin'],
    isActive: (pathName) => pathName.indexOf('/app/attendance') > -1,
  },
  {
    title: 'Leaves',
    link: '/app/leaves',
    icon: <MonitorOff className="h-5 w-5" />,
    authorized: ['CompanyAdmin', 'Employee'],
    isActive: (pathName) => pathName.includes('/app/leaves'),
  },
  {
    title: 'Compensation',
    link: '/app/compensation',
    icon: <IndianRupee className="h-5 w-5" />,
    authorized: ['CompanyAdmin', 'Employee'],
    isActive: (pathName) => pathName.includes('/app/compensation'),
  },
  {
    title: 'Company Options',
    link: '/app/company-options',
    icon: <GripIcon className="h-5 w-5" />,
    authorized: ['CompanyAdmin'],
    isActive: (pathName) => pathName.includes('/app/company-options'),
  },
  {
    title: 'Salary',
    link: '/app/salary',
    icon: <GripIcon className="h-5 w-5" />,
    authorized: ['CompanyAdmin'],
    isActive: (pathName) => pathName.includes('/app/salary'),
  },
];

const NavMenu = () => {
  const { pathname } = useLocation();
  const { user } = useAuth();

  return (
    <aside className="fixed inset-y-0 left-0 z-10 hidden w-14 flex-col border-r bg-background sm:flex">
      <nav className="flex flex-col items-center gap-4 px-2 sm:py-4">
        <div className="pb-3">
          <Link to="/">
            <img src="/logo.png" alt="Bit Xplorer" />
            <span className="sr-only">Bit Xplorer</span>
          </Link>
        </div>

        {navMenuItems.map((m, i) => (
          <Tooltip key={i}>
            {_.some(user.roles, (x) => m.authorized.includes(x)) && (
              <>
                <TooltipTrigger asChild>
                  <Link
                    to={m.link}
                    className={cn(
                      'flex h-9 w-9 items-center justify-center rounded-lg text-muted-foreground transition-colors hover:text-foreground md:h-8 md:w-8',
                      m.isActive(pathname) ? 'bg-primary text-lg font-semibold text-primary-foreground md:text-base' : ''
                    )}
                  >
                    {m.icon}
                    <span className="sr-only">{m.title}</span>
                  </Link>
                </TooltipTrigger>
                <TooltipContent side="right">{m.title}</TooltipContent>
              </>
            )}
          </Tooltip>
        ))}
      </nav>

      <nav className="mt-auto flex flex-col items-center gap-4 px-2 sm:py-4">
        <Tooltip>
          <TooltipContent side="right">Settings</TooltipContent>
        </Tooltip>
        <ThemeToggle />
      </nav>
    </aside>
  );
};

export { NavMenu, navMenuItems };
