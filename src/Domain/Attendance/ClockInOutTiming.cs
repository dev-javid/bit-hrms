namespace Domain.Attendance
{
    public class ClockInOutTiming : CompanyEntity<int>
    {
        private ClockInOutTiming()
        {
        }

        public int EmployeeId { get; private set; }

        public DateOnly Date { get; private set; }

        public TimeOnly ClockInTime { get; private set; }

        public TimeOnly? ClockOutTime { get; private set; }

        internal static ClockInOutTiming Create(DateOnly date, TimeOnly clockInTime)
        {
            return new ClockInOutTiming
            {
                Date = date,
                ClockInTime = clockInTime,
            };
        }

        internal void SetClockOutTime(TimeOnly time)
        {
            ClockOutTime = time;
        }
    }
}
