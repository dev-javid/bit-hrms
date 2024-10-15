import { Package2, PanelLeft } from 'lucide-react';
import { Link, useLocation } from 'react-router-dom';
import { SheetTrigger, Button, SheetContent, Sheet, cn } from 'xplorer-ui';
import { navMenuItems } from './nav-menu';
import _ from 'lodash';
import useAuth from '@/lib/hooks/use-auth';

const NavMenuSmall = () => {
  const { pathname } = useLocation();
  const { user } = useAuth();

  return (
    <Sheet>
      <SheetTrigger asChild>
        <Button size="icon" variant="outline" className="sm:hidden">
          <PanelLeft className="h-5 w-5" />
          <span className="sr-only">Toggle Menu</span>
        </Button>
      </SheetTrigger>
      <SheetContent side="left" className="sm:max-w-xs">
        <nav className="grid gap-6 text-lg font-medium">
          <Link
            to="/"
            className="group flex h-10 w-10 shrink-0 items-center justify-center gap-2 rounded-full bg-emerald-300 text-lg font-semibold text-primary-foreground md:text-base"
          >
            <Package2 className="h-5 w-5 transition-all group-hover:scale-110" />
            <span className="sr-only">Bit Xplorer</span>
          </Link>
          {navMenuItems.map((m, i) =>
            _.some(user.roles, (x) => m.authorized.includes(x)) ? (
              <Link
                key={i}
                to={m.link}
                className={cn(
                  'flex items-center gap-4 px-2.5 text-muted-foreground hover:text-foreground',
                  m.isActive(pathname) ? 'bg-primary text-lg font-semibold text-primary-foreground md:text-base' : '',
                )}
              >
                {m.icon}
                {m.title}
              </Link>
            ) : null,
          )}
        </nav>
      </SheetContent>
    </Sheet>
  );
};

export default NavMenuSmall;
