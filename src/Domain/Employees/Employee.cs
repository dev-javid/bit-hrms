using Domain.Attendance;
using Domain.Companies;
using Domain.Salaries;

namespace Domain.Employees
{
    public class Employee : CompanyEntity<int>
    {
        private Employee()
        {
        }

        public int UserId { get; private set; }

        public int DepartmentId { get; private set; }

        public int JobTitleId { get; private set; }

        public string FirstName { get; private set; } = null!;

        public string LastName { get; private set; } = null!;

        public string FullName => $"{FirstName} {LastName}";

        public DateOnly DateOfBirth { get; private set; }

        public DateOnly DateOfJoining { get; private set; }

        public string FatherName { get; private set; } = null!;

        public PhoneNumber PhoneNumber { get; private set; } = null!;

        public Email CompanyEmail { get; private set; } = null!;

        public Email PersonalEmail { get; private set; } = null!;

        public string Address { get; private set; } = null!;

        public string City { get; private set; } = null!;

        public PAN PAN { get; private set; } = null!;

        public Aadhar Aadhar { get; private set; } = null!;

        public Department Department { get; private set; } = null!;

        public JobTitle JobTitle { get; private set; } = null!;

        public User User { get; private set; } = null!;

        public ICollection<EmployeeDocument> EmployeeDocuments { get; private set; } = [];

        public ICollection<EmployeeLeave> EmployeeLeaves { get; private set; } = [];

        public ICollection<ClockInOutTiming> ClockInOutTimings { get; private set; } = [];

        public ICollection<AttendanceRegularization> AttendanceRegularizations { get; private set; } = [];

        public ICollection<Compensation> Compensations { get; private set; } = [];

        public ICollection<Salary> Salaries { get; private set; } = null!;

        public static Employee Create(
            int userId,
            int departmentId,
            int jobTitleId,
            string firstName,
            string lastName,
            string fatherName,
            DateOnly dateOfBirth,
            DateOnly dateOfJoining,
            PhoneNumber phoneNumber,
            Email companyEmail,
            Email personalEmail,
            string address,
            string city,
            PAN pan,
            Aadhar aadhar)
        {
            return new Employee
            {
                UserId = userId,
                JobTitleId = jobTitleId,
                DepartmentId = departmentId,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                DateOfJoining = dateOfJoining,
                FatherName = fatherName,
                PhoneNumber = phoneNumber,
                CompanyEmail = companyEmail,
                PersonalEmail = personalEmail,
                Address = address,
                City = city,
                PAN = pan,
                Aadhar = aadhar,
            };
        }

        public EmployeeDocument SetDocument(FileName fileName, DocumentType documentType)
        {
            var document = EmployeeDocuments.FirstOrDefault(x => x.DocumentType == documentType);
            if (document != null)
            {
                document.Update(fileName);
            }
            else
            {
                document = EmployeeDocument.Create(fileName, documentType);
                EmployeeDocuments.Add(document);
            }

            return document;
        }

        public EmployeeLeave ApplyLeave(DateOnly from, DateOnly to, LeavePolicy leavePolicy, FinancialMonth financialMonth, IEnumerable<Holiday> holidays)
        {
            holidays.ToList().ForEach(holiday =>
            {
                if (from == holiday.Date || to == holiday.Date)
                {
                    throw CustomException.WithBadRequest("There is already a holiday on applied date.");
                }
            });

            foreach (var appliedLeave in EmployeeLeaves.ToList())
            {
                var date = from;
                while (date <= to)
                {
                    if (appliedLeave.ContainsDate(date))
                    {
                        throw CustomException.WithBadRequest($"A leave is already applied on '{date}'");
                    }

                    date = date.AddDays(1);
                }
            }

            var appliedDays = to.DayNumber - from.DayNumber + 1;
            if (appliedDays < 0)
            {
                throw CustomException.WithBadRequest("'From' cannot be after 'To'.");
            }

            var availableLeaves = GetAvailableLeaves(financialMonth, leavePolicy);
            if (availableLeaves <= 0)
            {
                throw CustomException.WithBadRequest("You have consumed all leaves.");
            }

            if (appliedDays > availableLeaves)
            {
                throw CustomException.WithBadRequest("You don't have sufficient leaves.");
            }

            var leave = EmployeeLeave.Create(from, to);
            EmployeeLeaves.Add(leave);
            return leave;
        }

        public double GetAvailableLeaves(FinancialMonth financialMonth, LeavePolicy leavePolicy)
        {
            var casualLeaves = GetCasualLeaves(financialMonth, leavePolicy);
            var earnedLeaves = GetEarnedLeaves(financialMonth, leavePolicy);
            var totalLeaves = casualLeaves + earnedLeaves;
            return totalLeaves - GetConsumedLeaves(financialMonth);
        }

        public int GetConsumedLeaves(FinancialMonth financialMonth)
        {
            return EmployeeLeaves
                .Where(x => x.Status != EmployeeLeave.LeaveStatus.Declined && x.CreatedOn > financialMonth.FirstDay())
                .Sum(x =>
                {
                    return x.To.DayNumber - x.From.DayNumber + 1;
                });
        }

        public void DeleteLeave(int employeeLeaveId)
        {
            var leave = EmployeeLeaves.FirstOrDefault(x => x.Id == employeeLeaveId);
            if (leave == null)
            {
                throw CustomException.WithNotFound("Leave not found");
            }

            if (leave.Status != EmployeeLeave.LeaveStatus.Pending)
            {
                throw CustomException.WithNotFound("Leave cannot be deleted");
            }

            EmployeeLeaves.Remove(leave);
        }

        public void ClockIn(IEnumerable<Holiday> holidays)
        {
            if (holidays.Any(holiday => holiday.Date == DateTime.UtcNow.ToDateOnly()))
            {
                throw CustomException.WithBadRequest("Clock in is not allowed on holidays.");
            }

            if (DateTime.UtcNow.IsWeeklyOff())
            {
                throw CustomException.WithBadRequest("Clock in is not allowed on Week off.");
            }

            if (EmployeeLeaves.Any(leave => leave.Status == EmployeeLeave.LeaveStatus.Approved && leave.ContainsDate(DateTime.UtcNow.ToDateOnly())))
            {
                throw CustomException.WithBadRequest("Clock in is not allowed when leave is approved for today.");
            }

            if (ClockInOutTimings.Any(c => c.Date == DateTime.UtcNow.ToDateOnly()))
            {
                throw CustomException.WithBadRequest("You have already clocked in for today.");
            }

            var clockInOutTiming = ClockInOutTiming.Create(DateTime.UtcNow.ToDateOnly(), DateTime.UtcNow.ToTimeOnly());
            ClockInOutTimings.Add(clockInOutTiming);
        }

        public void Clockout(int workHours)
        {
            var clockInOutTiming = ClockInOutTimings.FirstOrDefault(x => x.Date == DateTime.UtcNow.ToDateOnly());

            if (clockInOutTiming == null)
            {
                throw CustomException.WithBadRequest("You have not clocked in today.");
            }

            if ((TimeOnly.FromDateTime(DateTime.UtcNow) - clockInOutTiming.ClockInTime).TotalHours < workHours)
            {
                throw CustomException.WithBadRequest($"You must work for at least {workHours} hours before clocking out.");
            }

            clockInOutTiming.SetClockOutTime(DateTime.UtcNow.ToTimeOnly());
        }

        public void ApproveRegularization(int attendanceRegularizationId)
        {
            var regularization = AttendanceRegularizations.FirstOrDefault(ar => ar.Id == attendanceRegularizationId);

            if (regularization == null)
            {
                throw CustomException.WithNotFound($"Attendance regularization with id {attendanceRegularizationId} not found.");
            }

            var clockInOutTiming = ClockInOutTimings.FirstOrDefault(x => x.Date == regularization.Date);

            if (clockInOutTiming == null)
            {
                clockInOutTiming = ClockInOutTiming.Create(regularization.Date, regularization.ClockInTime);
                clockInOutTiming.SetClockOutTime(regularization.ClockOutTime);
                ClockInOutTimings.Add(clockInOutTiming);
            }
            else
            {
                clockInOutTiming.SetClockOutTime(regularization.ClockOutTime);
            }

            regularization.Approve();
        }

        public AttendanceRegularization AddAttendanceRegularization(DateOnly date, TimeOnly clockInTime, TimeOnly clockOutTime, string reason, int workHours)
        {
            var clockInOutTiming = ClockInOutTimings.FirstOrDefault(x => x.Date == date);

            if (date > DateTime.UtcNow.ToDateOnly())
            {
                throw CustomException.WithBadRequest("Cannot regularize attendance for a future date.");
            }

            if ((clockOutTime - clockInTime).TotalHours < workHours)
            {
                throw CustomException.WithBadRequest($"{workHours} work hours are required for attendance regularization.");
            }

            if (clockInOutTiming?.ClockOutTime != null)
            {
                throw CustomException.WithBadRequest("Cannot regularize, attendance already available for this date.");
            }

            var attendanceRegularization = AttendanceRegularization.Create(Id, date, clockInTime, clockOutTime, reason);
            AttendanceRegularizations.Add(attendanceRegularization);
            return attendanceRegularization;
        }

        public Compensation AddCompensation(int employeeId, DateOnly effectiveFrom, int amount)
        {
            var exestingCompensation = Compensations
                .FirstOrDefault(c => c.EffectiveFrom >= effectiveFrom);

            if (exestingCompensation != null)
            {
                throw CustomException.WithBadRequest($"There is already a compensation effective from {exestingCompensation.EffectiveFrom}.");
            }

            var compensation = Compensation.Create(employeeId, effectiveFrom, amount);
            Compensations.Add(compensation);
            return compensation;
        }

        private static double GetEarnedLeaves(FinancialMonth financialMonth, LeavePolicy leavePolicy)
        {
            var monthsThisFinancialYear = DateTime.Now.Month - financialMonth.Value + 1;
            return monthsThisFinancialYear * leavePolicy.EarnedLeavesPerMonth;
        }

        private int GetCasualLeaves(FinancialMonth financialMonth, LeavePolicy leavePolicy)
        {
            var monthsThisFinancialYear = DateTime.Now.Month - financialMonth.Value + 1;
            var monthsSinceJoining = DateTime.Now.Month - DateOfJoining.Month;

            if (monthsSinceJoining < monthsThisFinancialYear)
            {
                var deductLeaves = (leavePolicy.CasualLeaves / 12) * (monthsThisFinancialYear - monthsSinceJoining);
                return leavePolicy.CasualLeaves - deductLeaves;
            }

            return leavePolicy.CasualLeaves;
        }
    }
}
