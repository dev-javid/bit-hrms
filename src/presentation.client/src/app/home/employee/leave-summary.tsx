import { useGetBasicEmployeeReportQuery } from '@/lib/rtk/rtk.reports';
import { PageSkeleton } from '@/lib/components';
import { Link } from 'react-router-dom';
import {
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
  Button,
} from 'xplorer-ui';

const LeaveSummary = ({
  employeeId,
  hideActions,
}: {
  employeeId?: number;
  hideActions: boolean;
}) => {
  const { data, isFetching } = useGetBasicEmployeeReportQuery({ employeeId });

  return (
    <Card className="w-full">
      <CardHeader>
        <CardTitle>Leave Summary</CardTitle>
        <CardDescription>{}</CardDescription>
      </CardHeader>
      <CardContent>
        <PageSkeleton isLoading={isFetching} rows={2}>
          <div className="grid gap-3">
            <ul className="grid gap-3">
              <li className="flex items-center justify-between">
                <span className="text-muted-foreground">Consumed leaves</span>
                <span>{data?.leavesConsumed}</span>
              </li>
              <li className="flex items-center justify-between">
                <span className="text-muted-foreground">Available Leaves</span>
                <span>{data?.leavesAvailable}</span>
              </li>
            </ul>
            {!hideActions && (
              <div className="flex justify-end">
                <Link to="leaves">
                  <Button className="p-0" variant="link">
                    Leave History
                  </Button>
                </Link>
              </div>
            )}
          </div>
        </PageSkeleton>
      </CardContent>
    </Card>
  );
};

export default LeaveSummary;
