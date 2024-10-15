import { addMonths, differenceInMonths } from 'date-fns';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from 'xplorer-ui';
import { DateOnly } from '../types';
import _ from 'lodash';

function getMonths(referenceDate: DateOnly): DateOnly[] {
  const result: DateOnly[] = [];
  const now = new Date();
  let months = differenceInMonths(now, referenceDate.toDate()) + 1;

  while (months > 0) {
    const year = addMonths(now, -months).getFullYear();
    const month = addMonths(now, -months + 1).getMonth() + 1;
    result.push(DateOnly.fromParts(year, month, 1));
    months -= 1;
  }

  return _.reverse(result);
}

const MonthsDropdown = ({
  onMonthChange,
  defaultValue,
  referenceDate,
}: {
  onMonthChange: (date: DateOnly) => void;
  defaultValue: DateOnly;
  referenceDate: DateOnly;
}) => {
  const months = getMonths(referenceDate);
  return (
    <Select defaultValue={defaultValue.toDateOnlyISOString()} onValueChange={(date) => onMonthChange(date.asDateOnly())}>
      <SelectTrigger className="w-[150px]">
        <SelectValue placeholder="Select Month" />
      </SelectTrigger>
      <SelectContent>
        {months.map((dateOnly) => (
          <SelectItem key={dateOnly.toDateOnlyISOString()} value={dateOnly.toDateOnlyISOString()}>
            {dateOnly.toMonthString()}
          </SelectItem>
        ))}
      </SelectContent>
    </Select>
  );
};

export default MonthsDropdown;
