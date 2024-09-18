export interface InOutTiming {
  employeeId: number;
  date: string;
  clockInTime: string;
  clockOutTime: string;
  status: InOutTimingStatus;
  regularization?: AttendanceRegularization;
}

export type InOutTimingStatus =
  | 'ClockedIn'
  | 'Approved'
  | 'WeeklyOff'
  | 'Holiday'
  | 'OnLeave'
  | 'ClockInMissing'
  | 'ClockOutMissing'
  | 'RegularizationRequested';

export interface AttendanceRegularization {
  attendanceRegularizationId: number;
  date: string;
  clockInTime: string;
  clockOutTime: string;
  reason: string;
  approved: InOutTimingStatus;
}
