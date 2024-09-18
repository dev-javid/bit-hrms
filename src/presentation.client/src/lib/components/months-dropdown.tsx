import { parseISO, startOfMonth, subMonths } from 'date-fns';
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from 'xplorer-ui';
import { DateOnly } from '../types';

function getPast12Months(): DateOnly[] {
  const result: DateOnly[] = [];
  const now = new Date();

  for (let i = 0; i < 12; i++) {
    const pastDate = subMonths(startOfMonth(now), i);
    const dateOnly = DateOnly.fromDate(pastDate);
    if (dateOnly.toDate() > parseISO('2024-06-01')) {
      result.push(dateOnly);
    }
  }

  return result;
}

const MonthsDropdown = ({
  onMonthChange,
  defaultValue,
}: {
  onMonthChange: (date: DateOnly) => void;
  defaultValue: DateOnly;
}) => {
  const months = getPast12Months();
  return (
    <Select
      defaultValue={defaultValue.toDateOnlyISOString()}
      onValueChange={(date) => onMonthChange(date.asDateOnly())}
    >
      <SelectTrigger className="w-[150px]">
        <SelectValue placeholder="Select Month" />
      </SelectTrigger>
      <SelectContent>
        {months.map((dateOnly) => (
          <SelectItem
            key={dateOnly.toDateOnlyISOString()}
            value={dateOnly.toDateOnlyISOString()}
          >
            {dateOnly.toMonthString()}
          </SelectItem>
        ))}
      </SelectContent>
    </Select>
  );
};

export default MonthsDropdown;
