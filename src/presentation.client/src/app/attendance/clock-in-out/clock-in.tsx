import { useClockInMutation } from '@/lib/rtk/rtk.attendance';
import { InOutTiming } from '@/lib/types';
import { convertUtcTimeToLocalTime } from '@/lib/utils';
import { ClockIcon } from 'lucide-react';
import { Button, toast, useSimpleConfirm } from 'xplorer-ui';

const ClockIn = ({ inOutTining }: { inOutTining?: InOutTiming }) => {
  const [clockIn] = useClockInMutation();
  const { showConfirm } = useSimpleConfirm();

  const onClockInClick = async () => {
    const confirmed = await showConfirm('Clock in', 'Are you sure you want to clock in for the day?');

    if (confirmed) {
      const response = await clockIn(null);

      if (!('error' in response)) {
        toast({
          variant: 'primary',
          title: 'Success ',
          description: 'You have clocked in successfully',
        });
      }
    }
  };
  return (
    <div className="flex items-center gap-5">
      <Button variant="outline" disabled={!!inOutTining} className="w-auto" onClick={onClockInClick}>
        <ClockIcon className="mr-2 h-4 w-4" />
        Clock In
      </Button>
      {inOutTining && (
        <>
          <div>{convertUtcTimeToLocalTime(inOutTining.clockInTime)}</div>
        </>
      )}
    </div>
  );
};

export default ClockIn;
