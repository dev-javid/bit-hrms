import { Button, DateRangePicker, toast } from 'xplorer-ui';
import { useState } from 'react';
import { differenceInDays } from 'date-fns';
import { useAddEmployeeLeaveMutation } from '@/lib/rtk/rtk.employee-leaves';
import { DateOnly } from '@/lib/types';

export default function ApplyLeave({ onSuccess }: { onSuccess: () => void }) {
  const [apply] = useAddEmployeeLeaveMutation();

  const [date, setDate] = useState<DateRange | undefined>();
  const [error, setError] = useState('');

  const onApplyClick = async () => {
    if (!date?.from) {
      setError('Please select from date');
    } else if (!date?.to) {
      setError('Please select to date');
    }

    const response = await apply({
      from: DateOnly.fromDate(date!.from!).toDateOnlyISOString(),
      to: DateOnly.fromDate(date!.to!).toDateOnlyISOString(),
    });

    if (!('error' in response)) {
      toast({
        variant: 'primary',
        title: 'Success ',
        description: 'Leave applied successfully',
      });
      onSuccess();
    }
  };

  return (
    <div className="space-y-4">
      <div>Duration</div>
      <DateRangePicker onChange={setDate} />
      <div>{date?.to && date.from && `${differenceInDays(date?.to, date?.from) + 1} day(s) selected`}</div>
      {error && <div className="text-red-700">{error}</div>}
      <Button onClick={onApplyClick}>Apply</Button>
    </div>
  );
}
