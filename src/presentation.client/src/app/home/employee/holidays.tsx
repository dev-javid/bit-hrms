import { useGetHolidaysQuery } from '@/lib/rtk/rtk.holidays';
import { PageSkeleton } from '@/lib/components';
import {
  Alert,
  AlertDescription,
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from 'xplorer-ui';

const Holidays = () => {
  const { data, isFetching } = useGetHolidaysQuery(null);

  return (
    <Card>
      <CardHeader>
        <CardTitle>Holidays</CardTitle>
      </CardHeader>
      <PageSkeleton isLoading={isFetching}>
        <CardContent className="space-y-1">
          {data?.items.map((holiday) => (
            <Alert key={holiday.holidayId}>
              <AlertDescription className="flex justify-between">
                <p> {holiday.name}</p>
                <p>{holiday.date.asDateOnly().toDayString()}</p>
              </AlertDescription>
            </Alert>
          ))}
        </CardContent>
      </PageSkeleton>
    </Card>
  );
};

export default Holidays;
