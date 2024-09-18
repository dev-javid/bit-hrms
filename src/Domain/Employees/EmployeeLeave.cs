namespace Domain.Employees
{
    public class EmployeeLeave : CompanyEntity<int>
    {
        private EmployeeLeave()
        {
        }

        public enum LeaveStatus
        {
            Approved,
            Pending,
            Declined
        }

        public int EmployeeId { get; private set; }

        public DateOnly From { get; private set; }

        public DateOnly To { get; private set; }

        public LeaveStatus Status { get; private set; }

        public string? Remarks { get; set; }

        public Employee Employee { get; private set; } = null!;

        public static EmployeeLeave Create(DateOnly from, DateOnly to)
        {
            return new EmployeeLeave
            {
                From = from,
                To = to,
                Status = LeaveStatus.Pending,
            };
        }

        public bool ContainsDate(DateOnly dateOnly)
        {
            return Status != LeaveStatus.Declined && To >= dateOnly && From <= dateOnly;
        }

        public void Approve()
        {
            Status = LeaveStatus.Approved;
        }

        public void Decline(string remarks)
        {
            Status = LeaveStatus.Declined;
            Remarks = remarks;
        }
    }
}
