import { DateOnly } from '.';

declare global {
  interface String {
    asDateOnly(): DateOnly;
  }
}

String.prototype.asDateOnly = function (): DateOnly {
  return DateOnly.fromDateOnlyISOString(this.toString());
};
