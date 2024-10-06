import { ChevronLeft, ChevronRight, Copy, CreditCard, MoreVertical, Truck } from 'lucide-react';
import {
  Button,
  DropdownMenu,
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuSeparator,
  DropdownMenuItem,
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardFooter,
  CardContent,
  Separator,
  Pagination,
  PaginationContent,
  PaginationItem,
  useSimpleModal,
} from 'xplorer-ui';
import { BreadCrumbProps, PageContainer, PageSkeleton } from '@/lib/components';
import { Link } from 'react-router-dom';
import { useGetBasicAdminReportQuery } from '@/lib/rtk/rtk.reports';
import LeavePolicyForm from './leave-policy';

const breadCrumb: BreadCrumbProps = {
  title: 'Company Options',
  to: '',
};

export default function CompanyOptions() {
  const { data, isFetching } = useGetBasicAdminReportQuery(null);
  const { showModal } = useSimpleModal();

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageSkeleton isLoading={isFetching}>
        <div className="grid flex-1 items-start gap-4 sm:py-0 md:gap-8 lg:grid-cols-3 xl:grid-cols-3">
          <div className="grid auto-rows-max items-start gap-4 md:gap-8 lg:col-span-2">
            <div className="grid gap-4 sm:grid-cols-2 md:grid-cols-6 lg:grid-cols-2 xl:grid-cols-6">
              <Card className="col-span-2">
                <CardHeader className="pb-2">
                  <CardDescription>Leave & Holidays</CardDescription>
                  <CardTitle className="text-4xl">{data?.holidays}</CardTitle>
                </CardHeader>
                <CardFooter className="grid auto-rows-max items-start gap-2">
                  <Button
                    className="w-full"
                    onClick={() => {
                      showModal('Leave Policy', <LeavePolicyForm />);
                    }}
                  >
                    Leave Policy
                  </Button>
                  <Link to="holidays">
                    <Button className="w-full">Holidays</Button>
                  </Link>
                </CardFooter>
              </Card>
              <Card className="col-span-2">
                <CardHeader className="pb-2">
                  <CardDescription>Departments</CardDescription>
                  <CardTitle className="text-4xl">{data?.departments}</CardTitle>
                </CardHeader>
                <CardFooter className="grid auto-rows-max items-start gap-2">
                  <Link to="departments">
                    <Button className="w-full">View Departments</Button>
                  </Link>
                  <Link to="job-titles">
                    <Button className="w-full">Job Titles</Button>
                  </Link>
                </CardFooter>
              </Card>
              <Card className="col-span-2">
                <CardHeader className="pb-2">
                  <CardDescription>Company Finance</CardDescription>
                </CardHeader>
                <CardFooter className="grid auto-rows-max items-start gap-2">
                  <Link to="income-sources">
                    <Button className="w-full">Income Sources</Button>
                  </Link>
                </CardFooter>
              </Card>
            </div>
          </div>
          <div>
            <Card className="overflow-hidden" x-chunk="dashboard-05-chunk-4">
              <CardHeader className="flex flex-row items-start bg-muted/50">
                <div className="grid gap-0.5">
                  <CardTitle className="group flex items-center gap-2 text-lg">
                    Order Oe31b70H
                    <Button size="icon" className="h-6 w-6 opacity-0 transition-opacity group-hover:opacity-100">
                      <Copy className="h-3 w-3" />
                      <span className="sr-only">Copy Order ID</span>
                    </Button>
                  </CardTitle>
                  <CardDescription>Date: November 23, 2023</CardDescription>
                </div>
                <div className="ml-auto flex items-center gap-1">
                  <Button size="sm" className="h-8 gap-1">
                    <Truck className="h-3.5 w-3.5" />
                    <span className="lg:sr-only xl:not-sr-only xl:whitespace-nowrap">Track Order</span>
                  </Button>
                  <DropdownMenu>
                    <DropdownMenuTrigger asChild>
                      <Button size="icon" className="h-8 w-8">
                        <MoreVertical className="h-3.5 w-3.5" />
                        <span className="sr-only">More</span>
                      </Button>
                    </DropdownMenuTrigger>
                    <DropdownMenuContent align="end">
                      <DropdownMenuItem>Edit</DropdownMenuItem>
                      <DropdownMenuItem>Export</DropdownMenuItem>
                      <DropdownMenuSeparator />
                      <DropdownMenuItem>Trash</DropdownMenuItem>
                    </DropdownMenuContent>
                  </DropdownMenu>
                </div>
              </CardHeader>
              <CardContent className="p-6 text-sm">
                <div className="grid gap-3">
                  <div className="font-semibold">Order Details</div>
                  <ul className="grid gap-3">
                    <li className="flex items-center justify-between">
                      <span className="text-muted-foreground">
                        Glimmer Lamps x <span>2</span>
                      </span>
                      <span>$250.00</span>
                    </li>
                    <li className="flex items-center justify-between">
                      <span className="text-muted-foreground">
                        Aqua Filters x <span>1</span>
                      </span>
                      <span>$49.00</span>
                    </li>
                  </ul>
                  <Separator className="my-2" />
                  <ul className="grid gap-3">
                    <li className="flex items-center justify-between">
                      <span className="text-muted-foreground">Subtotal</span>
                      <span>$299.00</span>
                    </li>
                    <li className="flex items-center justify-between">
                      <span className="text-muted-foreground">Shipping</span>
                      <span>$5.00</span>
                    </li>
                    <li className="flex items-center justify-between">
                      <span className="text-muted-foreground">Tax</span>
                      <span>$25.00</span>
                    </li>
                    <li className="flex items-center justify-between font-semibold">
                      <span className="text-muted-foreground">Total</span>
                      <span>$329.00</span>
                    </li>
                  </ul>
                </div>
                <Separator className="my-4" />
                <div className="grid grid-cols-2 gap-4">
                  <div className="grid gap-3">
                    <div className="font-semibold">Shipping Information</div>
                    <address className="grid gap-0.5 not-italic text-muted-foreground">
                      <span>Liam Johnson</span>
                      <span>1234 Main St.</span>
                      <span>Anytown, CA 12345</span>
                    </address>
                  </div>
                  <div className="grid auto-rows-max gap-3">
                    <div className="font-semibold">Billing Information</div>
                    <div className="text-muted-foreground">Same as shipping address</div>
                  </div>
                </div>
                <Separator className="my-4" />
                <div className="grid gap-3">
                  <div className="font-semibold">Customer Information</div>
                  <dl className="grid gap-3">
                    <div className="flex items-center justify-between">
                      <dt className="text-muted-foreground">Customer</dt>
                      <dd>Liam Johnson</dd>
                    </div>
                    <div className="flex items-center justify-between">
                      <dt className="text-muted-foreground">Email</dt>
                      <dd>
                        <a href="mailto:">liam@acme.com</a>
                      </dd>
                    </div>
                    <div className="flex items-center justify-between">
                      <dt className="text-muted-foreground">Phone</dt>
                      <dd>
                        <a href="tel:">+1 234 567 890</a>
                      </dd>
                    </div>
                  </dl>
                </div>
                <Separator className="my-4" />
                <div className="grid gap-3">
                  <div className="font-semibold">Payment Information</div>
                  <dl className="grid gap-3">
                    <div className="flex items-center justify-between">
                      <dt className="flex items-center gap-1 text-muted-foreground">
                        <CreditCard className="h-4 w-4" />
                        Visa
                      </dt>
                      <dd>**** **** **** 4532</dd>
                    </div>
                  </dl>
                </div>
              </CardContent>
              <CardFooter className="flex flex-row items-center border-t bg-muted/50 px-6 py-3">
                <div className="text-xs text-muted-foreground">
                  Updated <time dateTime="2023-11-23">November 23, 2023</time>
                </div>
                <Pagination className="ml-auto mr-0 w-auto">
                  <PaginationContent>
                    <PaginationItem>
                      <Button size="icon" className="h-6 w-6">
                        <ChevronLeft className="h-3.5 w-3.5" />
                        <span className="sr-only">Previous Order</span>
                      </Button>
                    </PaginationItem>
                    <PaginationItem>
                      <Button size="icon" className="h-6 w-6">
                        <ChevronRight className="h-3.5 w-3.5" />
                        <span className="sr-only">Next Order</span>
                      </Button>
                    </PaginationItem>
                  </PaginationContent>
                </Pagination>
              </CardFooter>
            </Card>
          </div>
        </div>
      </PageSkeleton>
    </PageContainer>
  );
}
