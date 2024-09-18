namespace Domain.Companies
{
    public class LeavePolicy : CompanyEntity<int>
    {
        private LeavePolicy()
        {
        }

        public int CasualLeaves { get; private set; }

        public double EarnedLeavesPerMonth { get; private set; }

        public int Holidays { get; private set; }

        internal static LeavePolicy Create(int casualLeaves, double earnedLeavesPrMonth, int holidays)
        {
            return new LeavePolicy
            {
                CasualLeaves = casualLeaves,
                EarnedLeavesPerMonth = earnedLeavesPrMonth,
                Holidays = holidays
            };
        }

        internal void Update(int casualLeaves, double earnedLeavesPrMonth, int holidays)
        {
            CasualLeaves = casualLeaves;
            EarnedLeavesPerMonth = earnedLeavesPrMonth;
            Holidays = holidays;
        }
    }
}
