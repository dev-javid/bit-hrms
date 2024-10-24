import { ActionColumn, DataTableColumnHeader } from 'xplorer-ui';
import { Employee } from '@/lib/types';
import { ColumnDef } from '@tanstack/react-table';
import { FileText, IndianRupee } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import { AddButton } from '@/lib/components';

const useColumns = () => {
  const navigateTo = useNavigate();

  const columns: ColumnDef<Employee>[] = [
    {
      accessorKey: 'employeeId',
      header: ({ column }) => <DataTableColumnHeader column={column} title="ID" />,
      enableSorting: false,
    },
    {
      accessorKey: 'name',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Name" />,
      cell: ({ row }) => {
        return (
          <div className="flex space-x-2">
            <span className="max-w-[500px] truncate font-medium">{`${row.original['firstName']} ${row.original['lastName']}`}</span>
          </div>
        );
      },
    },
    {
      accessorKey: 'jobTitleName',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Job Title" />,
      enableSorting: false,
    },
    {
      accessorKey: 'companyEmail',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Email" />,
      enableSorting: false,
    },
    {
      accessorKey: 'dateOfJoining',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Joined On" />,
      cell: ({ row }) => <div className="w-[80px]">{row.original['dateOfJoining'].asDateOnly().toDayString()}</div>,
      enableSorting: false,
    },
    {
      accessorKey: 'compensation',
      header: () => <div className="flex gap-2 justify-end">Compensation</div>,
      cell: ({ row }) => {
        return (
          <div className="flex justify-end">
            {row.original.compensation ? (
              <div
                className="flex justify-end cursor-pointer text-gray-500 hover:text-foreground"
                onClick={() => navigateTo(`/app/compensation/`, { state: { employee: row.original } })}
              >
                <IndianRupee size={12} className="mt-1" />
                <span>{row.original.compensation.toFixed(2)}</span>
              </div>
            ) : (
              <AddButton
                text="Add Compensation"
                to="/app/compensation/"
                className="px-0"
                state={{ employee: row.original }}
                tooltip="Add compensation"
              />
            )}
          </div>
        );
      },
    },
    {
      ...ActionColumn({
        placement: 'right',
        onEditClick: (employee) => navigateTo(`/app/employees/${employee.employeeId}`),
        onDeleteClick: () => alert('delete clicked'),
        editIconSize: 16,
        deleteIconSize: 16,
        otherActions: [
          {
            icon: <FileText size={16} />,
            toolTip: 'Documents',
            onClick: (employee) => navigateTo(`/app/employees/${employee.employeeId}/documents`),
          },
        ],
      }),
    },
  ];

  return columns;
};

export default useColumns;
