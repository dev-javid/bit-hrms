import { DateOnly } from '.';

declare global {
  interface String {
    asDateOnly(): DateOnly;
  }
}
