import {
  DataTableColumnHeader,
  Tooltip,
  TooltipContent,
  TooltipTrigger,
  useSimpleModal,
} from 'xplorer-ui';
import { Employee } from '@/lib/types';
import { ColumnDef, Row } from '@tanstack/react-table';
import {
  CalendarDays,
  FileIcon,
  IndianRupee,
  MonitorOff,
  UploadIcon,
} from 'lucide-react';
import { Link } from 'react-router-dom';
import {
  DropdownMenu,
  DropdownMenuTrigger,
  Button,
  DropdownMenuContent,
  DropdownMenuCheckboxItem,
} from 'xplorer-ui';
import DocumentForm from '../document-form';

const DocumentsCell = ({ row }: { row: Row<Employee> }) => {
  const { showModal } = useSimpleModal();

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button variant="outline" size="sm">
          {row.original.documents.length == 2 ? (
            <FileIcon size={20} />
          ) : (
            <UploadIcon size={20} />
          )}
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent align="end">
        <DropdownMenuCheckboxItem
          onClick={() =>
            showModal(
              'Upload Document',
              <DocumentForm employee={row.original} />
            )
          }
        >
          Upload new
        </DropdownMenuCheckboxItem>
        {row.original.documents.map((d) => (
          <DropdownMenuCheckboxItem
            checked
            key={d.type}
            onClick={() => {
              showModal(
                'Upload Document',
                <DocumentForm employee={row.original} document={d} />
              );
            }}
          >
            {d.type}
          </DropdownMenuCheckboxItem>
        ))}
      </DropdownMenuContent>
    </DropdownMenu>
  );
};

export const columns: ColumnDef<Employee>[] = [
  {
    accessorKey: 'employeeId',
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="ID" />
    ),
    enableSorting: false,
  },
  {
    accessorKey: 'name',
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Name" />
    ),
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
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Job Title" />
    ),
    enableSorting: false,
  },
  {
    accessorKey: 'companyEmail',
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Email" />
    ),
    enableSorting: false,
  },
  {
    header: 'Documents',
    accessorKey: 'documents',
    cell: DocumentsCell,
  },
  {
    accessorKey: 'dateOfJoining',
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Joined On" />
    ),
    cell: ({ row }) => (
      <div className="w-[80px]">
        {row.original['dateOfJoining'].asDateOnly().toDayString()}
      </div>
    ),
    enableSorting: false,
  },
  {
    accessorKey: 'city',
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Go to" />
    ),
    enableSorting: false,
    cell: ({ row }) => (
      <div className="flex gap-2">
        <Tooltip>
          <TooltipTrigger>
            <Link to="/app/attendance" state={{ employee: row.original }}>
              <Button size="sm" variant="outline" className="text-primary">
                <CalendarDays size={20} />
              </Button>
            </Link>
          </TooltipTrigger>
          <TooltipContent>
            <p>Attendance</p>
          </TooltipContent>
        </Tooltip>
        <Tooltip>
          <TooltipTrigger>
            <Link to="">
              <Button size="sm" variant="outline" className="text-primary">
                <MonitorOff size={20} />
              </Button>
            </Link>
          </TooltipTrigger>
          <TooltipContent>
            <p>Leaves</p>
          </TooltipContent>
        </Tooltip>
        <Tooltip>
          <TooltipTrigger>
            <Link to="/app/compensation" state={{ employee: row.original }}>
              <Button size="sm" variant="outline" className="text-primary">
                <IndianRupee size={20} />
              </Button>
            </Link>
          </TooltipTrigger>
          <TooltipContent>
            <p>Compensation</p>
          </TooltipContent>
        </Tooltip>
      </div>
    ),
  },
];
