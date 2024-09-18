import { useGetBasicAdminReportQuery } from '@/lib/rtk/rtk.reports';
import { PageSkeleton } from '@/lib/components';
import { Building2, LayoutPanelTop, MonitorOff } from 'lucide-react';
import { Link } from 'react-router-dom';
import {
  Card,
  CardHeader,
  CardDescription,
  CardTitle,
  CardFooter,
  Button,
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from 'xplorer-ui';

const items = [
  {
    name: 'Departments',
    icon: <LayoutPanelTop className="mr-1 h-3.5 w-3.5" />,
    children: [
      {
        name: 'Department List',
        to: 'administration/departments',
      },
      {
        name: 'Create Department',
        to: 'administration/departments/0',
      },
    ],
  },
  {
    name: 'Leaves & Holidays',
    icon: <MonitorOff className="mr-1 h-3.5 w-3.5" />,
    children: [
      {
        name: 'Leave Policy',
        to: 'administration/leave-policy',
      },
      {
        name: 'Holidays',
        to: 'administration/holidays',
      },
    ],
  },
  {
    name: 'Companies',
    icon: <Building2 className="mr-1 h-3.5 w-3.5" />,
    children: [
      {
        name: 'Company List',
        to: 'administration/companies',
      },
      {
        name: 'Create Company',
        to: 'administration/companies/0',
      },
    ],
  },
];

const AdministrationLinks = () => {
  const { data, isFetching } = useGetBasicAdminReportQuery(null);
  return (
    <PageSkeleton isLoading={isFetching}>
      <Card>
        <CardHeader className="pb-2">
          <CardDescription>Employees</CardDescription>
          <CardTitle className="text-4xl">{data?.employees}</CardTitle>
        </CardHeader>
        <CardFooter className="grid auto-rows-max items-start gap-2">
          {items.map((item) => (
            <DropdownMenu key={item.name}>
              <DropdownMenuTrigger asChild>
                <Button size="sm" className="w-full">
                  {item.icon}
                  <span className=" w-full lg:sr-only xl:not-sr-only xl:whitespace-nowrap">
                    {item.name}
                  </span>
                </Button>
              </DropdownMenuTrigger>
              <DropdownMenuContent align="end">
                {item.children.map((child) => (
                  <DropdownMenuItem key={child.name}>
                    <Link to={child.to}>{child.name}</Link>
                  </DropdownMenuItem>
                ))}
              </DropdownMenuContent>
            </DropdownMenu>
          ))}
        </CardFooter>
      </Card>
    </PageSkeleton>
  );
};

export default AdministrationLinks;
