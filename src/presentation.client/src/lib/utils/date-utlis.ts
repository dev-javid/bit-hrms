import { differenceInMinutes, isSaturday, addDays, differenceInDays, isWithinInterval, parseISO, format, toDate } from 'date-fns';
import { DateOnly } from '../types';

const regex = /^(\d{2}:\d{2})/;
const refOffSaturday = '2024-04-21';

export const isWithinDateRange = (from: string, to: string, value: string): boolean => {
  const dateFrom = parseISO(from);
  const dateTo = parseISO(to);
  const dateValue = parseISO(value);

  return isWithinInterval(dateValue, { start: dateFrom, end: dateTo });
};

export const months = {
  January: 1,
  February: 2,
  March: 3,
  April: 4,
  May: 5,
  June: 6,
  July: 7,
  August: 8,
  September: 9,
  October: 10,
  November: 11,
  December: 12,
};

const convertUtcTimeToLocalDate = (time: string) => {
  const match = time?.match(regex);
  const currentDate = format(new Date(), 'yyyy-MM-dd');
  const isoString = `${currentDate}T${match![1]}:00`;
  const date = parseISO(isoString);
  return toDate(date.toUTCString());
};

export const convertUtcTimeToLocalTime = (time: string) => {
  return format(convertUtcTimeToLocalDate(time), 'HH:mm');
};

export const timeDifference = (fromTime: string, toTime: string) => {
  const fromDateTime = convertUtcTimeToLocalDate(fromTime);
  const toDateTime = convertUtcTimeToLocalDate(toTime);

  const diffInMinuites = differenceInMinutes(toDateTime, fromDateTime);

  const hours = Math.floor(diffInMinuites / 60);
  const minutes = diffInMinuites % 60;
  return hours > 0 ? `${hours} hours ${minutes} minutes` : `${minutes} minutes`;
};

export function getNextSaturday(): DateOnly {
  const today = new Date();
  let nextSaturday = addDays(today, 1);
  while (!isSaturday(nextSaturday)) {
    nextSaturday = addDays(nextSaturday, 1);
  }
  return DateOnly.fromDate(nextSaturday);
}

export const nextSaturdayWorking = () => {
  const refDate = refOffSaturday.asDateOnly().toDate();
  const nextSaturday = getNextSaturday().toDate();
  const daysPassed = differenceInDays(nextSaturday, refDate) + 1;
  return daysPassed % 14 != 0;
};

export const isWeekOff = (date: string) => {
  if (parseISO(date).getDay() == 0) {
    return true;
  }

  if (parseISO(date).getDay() == 6) {
    const refDate = refOffSaturday.asDateOnly().toDate();
    const toDate = date.asDateOnly().toDate();
    const daysPassed = differenceInDays(toDate, refDate) + 1;
    return daysPassed % 14 == 0;
  }

  return false;
};
