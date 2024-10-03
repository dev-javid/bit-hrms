using Domain.Employees;

namespace Tests.Unit.Domain.Employees
{
    public class EmployeeTests
    {
        private readonly Faker _faker = new();
        private readonly Employee _employee;
        private readonly Company _company;

        public EmployeeTests()
        {
            _employee = Employee.Create(
                _faker.Random.Int(),
                _faker.Random.Int(),
                _faker.Random.Int(),
                _faker.Name.FirstName(),
                _faker.Name.LastName(),
                _faker.Name.FullName(),
                DateOnly.FromDateTime(DateTime.Today),
                DateOnly.FromDateTime(DateTime.Today),
                "1234567890".ToValueObject<PhoneNumber>(),
                _faker.Internet.Email().ToValueObject<Email>(),
                _faker.Internet.Email().ToValueObject<Email>(),
                _faker.Address.StreetAddress(),
                _faker.Address.City(),
                "GNCPS7000S".ToValueObject<PAN>(),
                "496678543456".ToValueObject<Aadhar>());

            _company = Company.Create(
                _faker.Company.CompanyName(),
                _faker.Internet.Email().ToValueObject<Email>(),
                "1234567890".ToValueObject<PhoneNumber>(),
                _faker.Name.FullName(),
                _faker.Address.FullAddress());
        }

        [Fact]
        public void Clockout_Should_Throw_Exception_When_There_Is_No_Clock_In_For_To_Day()
        {
            //Act
            Action action = () => _employee.Clockout(2);

            //Assert
            action.Should().Throw<CustomException>()
                  .WithMessage("You have not clocked in today.")
                  .And
                  .Should().Match<CustomException>(ex => ex.HttpStatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public void Clockin_Should_Throw_Exception_When_There_Is_Already_Clock_In_For_To_Day()
        {
            if (DateTime.UtcNow.IsWeeklyOff())
            {
                // this test will fail on week offs so lets skip on week-off
                return;
            }

            //Arrange
            var holidays = _company.Holidays.ToList();

            _employee.ClockIn(holidays);

            //Act
            Action action = () => _employee.ClockIn(holidays);

            //Assert
            action.Should()
                  .Throw<CustomException>()
                  .WithMessage("You have already clocked in for today.")
                  .And
                  .Should().Match<CustomException>(ex => ex.HttpStatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public void AddAttendanceRegularization_Should_Throw_Exception_When_There_Is_Already_Attendance_Available_For_This_Date()
        {
            if (DateTime.UtcNow.IsWeeklyOff())
            {
                // this test will fail on week offs so lets skip on week-off
                return;
            }

            //Arrang
            var holidays = _company.Holidays.ToList();

            _employee.ClockIn(holidays);
            _employee.Clockout(0);

            //Act
            Action action = () => _employee.AddAttendanceRegularization(DateTime.UtcNow.ToDateOnly(), DateTime.UtcNow.ToTimeOnly(), DateTime.UtcNow.ToTimeOnly().AddHours(10), "some reason", 9);

            //Assert
            action.Should()
                  .Throw<CustomException>()
                  .WithMessage("Cannot regularize, attendance already available for this date.")
                  .And
                  .Should().Match<CustomException>(ex => ex.HttpStatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public void AddAttendanceRegularization_Should_Throw_Exception_When_Minimum_Hours_Not_Worked_Upon()
        {
            int workHouirs = 10;
            if (DateTime.UtcNow.IsWeeklyOff())
            {
                // this test will fail on week offs so lets skip on week-off
                return;
            }

            //Arrang
            var holidays = _company.Holidays.ToList();

            _employee.ClockIn(holidays);
            _employee.Clockout(0);

            //Act
            Action action = () => _employee.AddAttendanceRegularization(DateTime.UtcNow.ToDateOnly(), DateTime.UtcNow.ToTimeOnly(), DateTime.UtcNow.ToTimeOnly().AddHours(9), "some reason", workHouirs);

            //Assert
            action.Should()
                  .Throw<CustomException>()
                  .WithMessage($"{workHouirs} work hours are required for attendance regularization.")
                  .And
                  .Should().Match<CustomException>(ex => ex.HttpStatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public void AddAttendanceRegularization_Should_Throw_Exception_When_Employee_Apply_Regularization_For_Future_Date()
        {
            if (DateTime.UtcNow.IsWeeklyOff())
            {
                // this test will fail on week offs so lets skip on week-off
                return;
            }

            //Arrang
            var holidays = _company.Holidays.ToList();

            _employee.ClockIn(holidays);

            //Act
            Action action = () => _employee.AddAttendanceRegularization(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)), TimeOnly.FromDateTime(DateTime.UtcNow), TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(9)), "some reason", 9);

            //Assert
            action.Should()
                  .Throw<CustomException>()
                  .WithMessage("Cannot regularize attendance for a future date.")
                  .And
                  .Should().Match<CustomException>(ex => ex.HttpStatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public void ApproveRegularization_Should_Throw_Exception_When_Regularization_Not_Found()
        {
            // Arrange
            int invalidId = -1;

            // Act
            Action action = () => _employee.ApproveRegularization(invalidId);

            // Assert
            action.Should().Throw<CustomException>()
                .WithMessage($"Attendance regularization with ID {invalidId} not found.")
                .And
                .Should().Match<CustomException>(ex => ex.HttpStatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public void AddCompensation_Should_Throw_Exception_When_There_Is_Already_A_Future_Compensation()
        {
            //Arrange
            var effectiveFrom = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(5));
            _employee.AddCompensation(1, effectiveFrom, 1000);

            // Act
            Action action = () => _employee.AddCompensation(1, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2)), 1000);

            // Assert
            action.Should().Throw<CustomException>()
                .WithMessage($"There is already a compensation effective from {effectiveFrom}.")
                .And
                .Should().Match<CustomException>(ex => ex.HttpStatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public void ClockIn_Should_Throw_Exception_When_Clock_In_Is_Attempted_On_Holiday()
        {
            _company.AddHoliday("Test Holiday", DateOnly.FromDateTime(DateTime.UtcNow), false);

            var holidays = _company.Holidays.ToList();

            // Act
            Action action = () => _employee.ClockIn(holidays);

            // Assert
            action.Should().Throw<CustomException>()
                  .WithMessage("Clock in is not allowed on holidays.")
                  .And
                  .Should().Match<CustomException>(ex => ex.HttpStatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public void ClockIn_Should_Throw_Exception_When_Leave_Is_Approved_For_Today()
        {
            if (DateTime.UtcNow.IsWeeklyOff())
            {
                // this test will fail on week offs so lets skip on week-off
                return;
            }

            // Arrange
            var holidays = new List<Holiday>();

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var employeeLeave = EmployeeLeave.Create(today, today.AddDays(2));
            employeeLeave.Approve();

            _employee.EmployeeLeaves.Add(employeeLeave);

            Action action = () => _employee.ClockIn(holidays);

            // Assert
            action.Should().Throw<CustomException>()
                  .WithMessage("Clock in is not allowed when leave is approved for today.")
                  .And
                  .Should().Match<CustomException>(ex => ex.HttpStatusCode == HttpStatusCode.BadRequest);
        }
    }
}
