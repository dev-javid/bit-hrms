using Domain.Employees;
using Domain.Finance;

namespace Domain.Companies
{
    public class Company : Entity<int>
    {
        private Company()
        {
            Employees = [];
            Departments = [];
            Holidays = [];
            IncomeSources = [];
            Expenses = [];
            WeeklyOffDays = [];
        }

        public int OwnerUserId { get; private set; }

        public string Name { get; private set; } = null!;

        public PhoneNumber PhoneNumber { get; private set; } = null!;

        public FinancialMonth FinancialMonth { get; private set; } = null!;

        public string AdministratorName { get; private set; } = null!;

        public Email Email { get; private set; } = null!;

        public string Address { get; private set; } = null!;

        public bool IsDeleted { get; private set; }

        public LeavePolicy? LeavePolicy { get; private set; }

        public ICollection<Employee> Employees { get; private set; }

        public ICollection<Department> Departments { get; private set; }

        public ICollection<Holiday> Holidays { get; private set; }

        public int CurrentUtilizableHolidays => Holidays.Count(x => !x.Optional) + (Holidays.Any(x => x.Optional) ? 1 : 0);

        public ICollection<IncomeSource> IncomeSources { get; private set; }

        public ICollection<Expense> Expenses { get; private set; }

        public DayOfWeek[] WeeklyOffDays { get; private set; }

        public static Company Create(int ownerUserId, string name, Email email, PhoneNumber phoneNumber, string administratorName, string address)
        {
            return new Company
            {
                OwnerUserId = ownerUserId,
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber,
                AdministratorName = administratorName,
                Address = address,
                FinancialMonth = 1.ToValueObject<FinancialMonth>(),
                WeeklyOffDays = []
            };
        }

        public void Update(string name, string administratorName, PhoneNumber phoneNumber, string address)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            AdministratorName = administratorName;
        }

        public void SetLeavePolicy(int casualLeaves, double earnedLeavesPrMonth, int holidays)
        {
            if (LeavePolicy is null)
            {
                LeavePolicy = LeavePolicy.Create(casualLeaves, earnedLeavesPrMonth, holidays);
            }
            else
            {
                LeavePolicy.Update(casualLeaves, earnedLeavesPrMonth, holidays);
            }
        }

        public Department AddDepartment(string name)
        {
            if (Departments.Any(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw CustomException.WithBadRequest("Department with same name already exists.");
            }

            var department = Department.Create(name);
            Departments.Add(department);
            return department;
        }

        public Department UpdateDepartment(int departmentId, string name)
        {
            var department = Departments.FirstOrDefault(x => x.Id == departmentId);
            if (department == null)
            {
                throw CustomException.WithNotFound("Department not found.");
            }

            if (Departments.Any(x => x.Id != departmentId && x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw CustomException.WithBadRequest("Another department with same name already exists.");
            }

            department.SetName(name);
            return department;
        }

        public Holiday UpdateHoliday(int holidayId, string name, DateOnly date, bool optional)
        {
            if (LeavePolicy is null)
            {
                throw CustomException.WithBadRequest("Please setup a leave policy before adding holiday.");
            }

            var holiday = Holidays.FirstOrDefault(x => x.Id == holidayId);
            if (holiday == null)
            {
                throw CustomException.WithNotFound("Holiday not found.");
            }

            if (holiday.Optional && !optional && CurrentUtilizableHolidays >= LeavePolicy.Holidays)
            {
                throw CustomException.WithBadRequest($"Maximum holiday limit reached. According to your leave policy, you are allowed a total of {LeavePolicy.Holidays} holidays.");
            }

            if (Holidays.Any(x => x.Id != holidayId && x.Date.Equals(date)))
            {
                throw CustomException.WithBadRequest("Another holiday on same date already exists.");
            }

            if (Holidays.Any(x => x.Id != holidayId && x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw CustomException.WithBadRequest("Another holiday with same name already exists.");
            }

            holiday.Update(name, date, optional);
            return holiday;
        }

        public Holiday AddHoliday(string name, DateOnly date, bool optional)
        {
            if (LeavePolicy is null)
            {
                throw CustomException.WithBadRequest("Please setup a leave policy before adding holiday.");
            }

            if (!optional && CurrentUtilizableHolidays >= LeavePolicy.Holidays)
            {
                throw CustomException.WithBadRequest($"Maximum holiday limit reached. According to your leave policy, you are allowed a total of {LeavePolicy.Holidays} holidays.");
            }

            if (Holidays.Any(x => x.Date == date))
            {
                throw CustomException.WithBadRequest("Holiday already exists for this date.");
            }

            if (Holidays.Any(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) && x.Date.Year == date.Year))
            {
                throw CustomException.WithBadRequest($"Same holiday(${name}) already exists for this year.");
            }

            var holiday = Holiday.Create(name, date, optional);
            Holidays.Add(holiday);
            return holiday;
        }

        public JobTitle AddJobTitle(string name, int departmentId)
        {
            var department = Departments.FirstOrDefault(x => x.Id == departmentId);
            if (department == null)
            {
                throw CustomException.WithNotFound("Department not found.");
            }

            return department.AddJobTitle(name);
        }

        public JobTitle UpdateJobTitle(int jobTitleId, string name)
        {
            var department = Departments
                .FirstOrDefault(x => x.JobTitles.Any(j => j.Id == jobTitleId));

            if (department is null)
            {
                throw CustomException.WithNotFound("Department not found.");
            }

            return department.UpdateJobTitle(jobTitleId, name);
        }

        public IncomeSource AddIncomeSource(string name, string description)
        {
            if (IncomeSources.Any(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw CustomException.WithBadRequest("Income source with same name already exists.");
            }

            var incomeSource = IncomeSource.Create(name, description);
            IncomeSources.Add(incomeSource);
            return incomeSource;
        }

        public void UpdateIncomeSource(int incomeSourceId, string name, string description)
        {
            var incomeSource = IncomeSources
                .FirstOrDefault(x => x.Id == incomeSourceId);

            if (incomeSource is null)
            {
                throw CustomException.WithNotFound("Income source not found.");
            }

            if (IncomeSources.Any(x => x.Id != incomeSourceId && x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw CustomException.WithBadRequest("Another income source with same name already exists.");
            }

            incomeSource.Update(name, description);
        }

        public void AddExpense(decimal requestAmount, string requestPurpose, IEnumerable<FileName> fileNames)
        {
            var expense = Expense.Create(requestAmount, requestPurpose, fileNames);
            Expenses.Add(expense);
        }

        public void SoftDelete()
        {
            IsDeleted = true;
        }
    }
}
