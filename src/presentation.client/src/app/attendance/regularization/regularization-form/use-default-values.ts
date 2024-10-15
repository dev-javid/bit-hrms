import { InOutTiming } from '@/lib/types';
import { FormSchemaType } from './schema';
import { convertUtcTimeToLocalTime } from '@/lib/utils';

const regex = /^(\d{2}:\d{2})/;

const useDefaultValues = (inoutTiming: InOutTiming) => {
  const clockInTime = inoutTiming.clockInTime ? convertUtcTimeToLocalTime(inoutTiming.clockInTime!) : '';
  const match = clockInTime?.match(regex);

  const defaultValues: FormSchemaType = {
    clockInTime: match ? match[1] : '',
    clockOutTime: inoutTiming.clockOutTime,
    date: inoutTiming.date,
    reason: '',
  };

  return {
    defaultValues,
  };
};

export default useDefaultValues;
