import { ActionColumn, DataTableColumnHeader, useSimpleModal } from 'xplorer-ui';
import { Employee } from '@/lib/types';
import { ColumnDef, Row } from '@tanstack/react-table';
import { CalendarDays, FileIcon, IndianRupee, MonitorOff, UploadIcon } from 'lucide-react';
import { DropdownMenu, DropdownMenuTrigger, Button, DropdownMenuContent, DropdownMenuCheckboxItem } from 'xplorer-ui';
import DocumentForm from '../document-form';
import { useNavigate } from 'react-router-dom';

const DocumentsCell = ({ row }: { row: Row<Employee> }) => {
  const { showModal } = useSimpleModal();

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button variant="outline" size="sm">
          {row.original.documents.length == 2 ? <FileIcon size={20} /> : <UploadIcon size={20} />}
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent align="end">
        <DropdownMenuCheckboxItem onClick={() => showModal('Upload Document', <DocumentForm employee={row.original} />)}>
          Upload new
        </DropdownMenuCheckboxItem>
        {row.original.documents.map((d) => (
          <DropdownMenuCheckboxItem
            checked
            key={d.type}
            onClick={() => {
              showModal('Upload Document', <DocumentForm employee={row.original} document={d} />);
            }}
          >
            {d.type}
          </DropdownMenuCheckboxItem>
        ))}
      </DropdownMenuContent>
    </DropdownMenu>
  );
};

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
      header: 'Documents',
      accessorKey: 'documents',
      cell: DocumentsCell,
    },
    {
      accessorKey: 'dateOfJoining',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Joined On" />,
      cell: ({ row }) => <div className="w-[80px]">{row.original['dateOfJoining'].asDateOnly().toDayString()}</div>,
      enableSorting: false,
    },
    {
      ...ActionColumn({
        onEditClick: () => alert('edit clicked'),
        onDeleteClick: () => alert('delete clicked'),
        otherActions: [
          {
            icon: <IndianRupee scale={16} />,
            toolTip: 'Salary',
            onClick: (employee) => navigateTo(`/app/salary/`, { state: { employee } }),
          },
          {
            icon: <CalendarDays scale={16} />,
            toolTip: 'Attendance',
            onClick: (employee) => navigateTo(`/app/attendance/`, { state: { employee } }),
          },
          {
            icon: <MonitorOff scale={16} />,
            toolTip: 'Leave',
            onClick: (employee) => navigateTo(`/app/leave/`, { state: { employee } }),
          },
        ],
      }),
    },
  ];

  return columns;
};

export default useColumns;
