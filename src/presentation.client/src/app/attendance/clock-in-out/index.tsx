import { useGetAttendanceQuery } from '@/lib/rtk/rtk.attendance';
import { PageSkeleton } from '@/lib/components';
import { Card, CardHeader, CardTitle, CardContent } from 'xplorer-ui';
import ClockIn from './clock-in';
import ClockOut from './clock-out';
import { DateOnly } from '@/lib/types';

const ClockInOut = () => {
  const { data, isLoading, isFetching } = useGetAttendanceQuery({
    date: DateOnly.today().toDateOnlyISOString(),
  });

  return (
    <PageSkeleton isLoading={isLoading || isFetching}>
      <Card>
        <CardHeader>
          <CardTitle>Clock</CardTitle>
        </CardHeader>
        <CardContent>
          <ClockIn inOutTining={data?.[0]} />
          <ClockOut inOutTining={data?.[0]} />
        </CardContent>
      </Card>
    </PageSkeleton>
  );
};

export default ClockInOut;
