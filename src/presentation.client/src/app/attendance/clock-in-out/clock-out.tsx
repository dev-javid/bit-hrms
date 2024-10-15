import { useClockOutMutation } from '@/lib/rtk/rtk.attendance';
import { InOutTiming } from '@/lib/types';
import { timeDifference, convertUtcTimeToLocalTime } from '@/lib/utils';
import { ClockIcon } from 'lucide-react';
import { Badge, Button, toast, useSimpleConfirm } from 'xplorer-ui';

const ClockOut = ({ inOutTining }: { inOutTining?: InOutTiming }) => {
  const [clockOut] = useClockOutMutation();
  const { showConfirm } = useSimpleConfirm();

  const onClockOutClick = async () => {
    const confirmed = await showConfirm('Clock out', 'Are you sure you want to clock out for the day?');

    if (confirmed) {
      const response = await clockOut(null);

      if (!('error' in response)) {
        toast({
          variant: 'primary',
          title: 'Success ',
          description: 'You have clocked out successfully',
        });
      }
    }
  };

  return (
    <div className="flex items-center gap-5 mt-5">
      <Button variant="outline" disabled={!inOutTining?.clockInTime || !!inOutTining?.clockOutTime} className="w-auto" onClick={onClockOutClick}>
        <ClockIcon className="mr-2 h-4 w-4" />
        Clock out
      </Button>
      {inOutTining?.clockOutTime && (
        <>
          <div>{convertUtcTimeToLocalTime(inOutTining.clockOutTime)}</div>
          <div>
            <Badge variant="secondary">You worked for {timeDifference(inOutTining.clockInTime, inOutTining?.clockOutTime)}</Badge>
          </div>
        </>
      )}
    </div>
  );
};

export default ClockOut;
