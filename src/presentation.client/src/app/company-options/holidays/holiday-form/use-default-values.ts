import { Holiday } from '@/lib/types';
import { FormSchemaType } from './schema';
import { useEffect, useState } from 'react';
import { useGetHolidaysQuery } from '@/lib/rtk/rtk.holidays';
import { useGetLeavePolicyQuery } from '@/lib/rtk/rtk.leave-policy';
import _ from 'lodash';

const useDefaultValues = (holiday?: Holiday) => {
  const [currentUtilizableHolidays, setCurrentUtilizableHolidays] = useState(0);
  const defaultValues: FormSchemaType = {
    holidayId: 0,
    date: '',
    name: '',
    optional: false,
  } as never as FormSchemaType;

  if (holiday) {
    defaultValues.holidayId = holiday.holidayId;
    defaultValues.name = holiday.name;
    defaultValues.date = holiday.date.asDateOnly().toDate();
    defaultValues.optional = holiday.optional;
  }

  const { data: holidays, isFetching: isFetchingHolidays, isLoading: isLoadingHolidays } = useGetHolidaysQuery(null);
  const { data: leavePolicy, isFetching: isFetchingLeavePolicy, isLoading: isLoadingLeavePolicy } = useGetLeavePolicyQuery(null);

  useEffect(() => {
    if (holidays?.items?.length) {
      const count = _.filter(holidays.items, { optional: false }).length + (_.some(holidays.items, { optional: true }) ? 1 : 0);
      setCurrentUtilizableHolidays(count);
    }
  }, [holidays]);

  return {
    formDefaultValues: defaultValues,
    isLoading: isLoadingHolidays || isLoadingLeavePolicy || isFetchingHolidays || isFetchingLeavePolicy,
    leavePolicy,
    holidays,
    currentUtilizableHolidays,
  };
};

export default useDefaultValues;
