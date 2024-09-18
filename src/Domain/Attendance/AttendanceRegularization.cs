using Domain.Employees;

namespace Domain.Attendance;

public class AttendanceRegularization : CompanyEntity<int>
{
    private AttendanceRegularization()
    {
    }

    public int EmployeeId { get; private set; }

    public DateOnly Date { get; private set; }

    public TimeOnly ClockInTime { get; private set; }

    public TimeOnly ClockOutTime { get; private set; }

    public string Reason { get; private set; } = null!;

    public bool Approved { get; private set; }

    public Employee Employee { get; private set; } = null!;

    public static AttendanceRegularization Create(
        int employeeId,
        DateOnly date,
        TimeOnly clockInTime,
        TimeOnly clockOutTime,
        string reason)
    {
        return new AttendanceRegularization
        {
            EmployeeId = employeeId,
            Date = date,
            ClockInTime = clockInTime,
            ClockOutTime = clockOutTime,
            Reason = reason,
        };
    }

    public void Approve()
    {
        Approved = true;
    }
}
