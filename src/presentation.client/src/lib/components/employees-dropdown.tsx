import { Container, Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from 'xplorer-ui';
import { useGetEmployeesQuery } from '@/lib/rtk/rtk.employees';
import { Employee } from '../types';

const EmployeeDropdown = ({
  onEmployeeSelect,
  selectedEmployeeId,
}: {
  onEmployeeSelect: (employee: Employee) => void;
  selectedEmployeeId?: number;
}) => {
  const { data, isLoading, isFetching } = useGetEmployeesQuery(null);

  return (
    <Container isLoading={isLoading || isFetching} rows={1}>
      {data && (
        <Select
          onValueChange={(employeeId) => onEmployeeSelect(data!.items.find((e) => e.employeeId == Number(employeeId))!)}
          value={selectedEmployeeId?.toString()}
        >
          <SelectTrigger className="w-[160px]">
            <SelectValue placeholder="Select Employee" />
          </SelectTrigger>
          <SelectContent>
            {data!.items.map((employee) => (
              <SelectItem key={employee.employeeId} value={employee.employeeId.toString()}>
                {employee.fullName}
              </SelectItem>
            ))}
          </SelectContent>
        </Select>
      )}
    </Container>
  );
};

export default EmployeeDropdown;
