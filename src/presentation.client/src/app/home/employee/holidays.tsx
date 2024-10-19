import { useGetHolidaysQuery } from '@/lib/rtk/rtk.holidays';
import { Alert, AlertDescription, Card, CardContent, CardHeader, CardTitle, Container } from 'xplorer-ui';


const Holidays = () => {
  const { data, isFetching } = useGetHolidaysQuery(null);

  return (
    <Card>
      <CardHeader>
        <CardTitle>Holidays</CardTitle>
      </CardHeader>
      <Container isLoading={isFetching}>
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
      </Container>
    </Card>
  );
};

export default Holidays;
