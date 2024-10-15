import { isValid, format, parseISO } from 'date-fns';
import { startOfMonth } from 'date-fns/fp';

export class DateOnly {
  public readonly year: number;
  public readonly month: number;
  public readonly day: number;

  private constructor(date: string | Date) {
    const parsedDate = this.parseDate(date);

    if (!parsedDate || !isValid(parsedDate)) {
      throw new Error('Invalid date');
    }

    this.year = parsedDate.getFullYear();
    this.month = parsedDate.getMonth() + 1;
    this.day = parsedDate.getDate();
  }

  private parseDate(date: string | Date): Date | null {
    if (date instanceof Date) {
      return date;
    }

    const parsedDate = parseISO(date);
    return isValid(parsedDate) ? parsedDate : null;
  }

  public static fromParts(year: number, month: number, day: number): DateOnly {
    if (month < 1 || month > 12) {
      throw new Error('Month must be between 1 and 12');
    }
    if (day < 1 || day > 31) {
      throw new Error('Day must be between 1 and 31');
    }
    if (day > new Date(year, month, 0).getDate()) {
      throw new Error('Day exceeds the number of days in the given month');
    }

    return new DateOnly(new Date(year, month - 1, day));
  }

  public static fromDateOnlyISOString(value: string): DateOnly {
    return new DateOnly(value);
  }

  public static fromDate(value: Date): DateOnly {
    return new DateOnly(value);
  }

  public static today(): DateOnly {
    return new DateOnly(new Date());
  }

  public static firstDayOfCurrentMonth(): DateOnly {
    const now = new Date();
    const firstDay = startOfMonth(now);
    return DateOnly.fromParts(firstDay.getFullYear(), firstDay.getMonth() + 1, firstDay.getDate());
  }

  toDate(): Date {
    return new Date(this.year, this.month - 1, this.day);
  }

  toDateOnlyISOString(): string {
    return format(this.toDate(), 'yyyy-MM-dd');
  }

  toMonthString(): string {
    return format(this.toDate(), 'MMM yyyy');
  }

  toDayString(): string {
    return format(this.toDate(), 'MMM d, yyyy');
  }
}
