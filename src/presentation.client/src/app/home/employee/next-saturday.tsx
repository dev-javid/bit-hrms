import { getNextSaturday, nextSaturdayWorking } from '@/lib/utils';
import { Laugh, Smile } from 'lucide-react';
import { Alert, AlertDescription, Card, CardContent, CardDescription, CardHeader, CardTitle } from 'xplorer-ui';

const NextSaturday = () => {
  return (
    <Card>
      <CardHeader>
        <CardTitle>Next Saturday</CardTitle>
        <CardDescription>{getNextSaturday().toMonthString()}</CardDescription>
      </CardHeader>
      <CardContent>
        <>
          <Alert>
            {nextSaturdayWorking() ? (
              <AlertDescription className="text-2xl flex justify-around">
                <span> Next Saturday is working day</span>
                <Smile size={'1.5em'} />
              </AlertDescription>
            ) : (
              <AlertDescription className="text-2xl flex justify-around">
                <span> Next Saturday is off day!!</span>
                <Laugh size={'1.5em'} />
              </AlertDescription>
            )}
          </Alert>
        </>
      </CardContent>
    </Card>
  );
};

export default NextSaturday;
