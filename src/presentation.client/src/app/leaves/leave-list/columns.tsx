import { DataTableColumnHeader } from 'xplorer-ui';
import { EmployeeLeave } from '@/lib/types';
import { ColumnDef } from '@tanstack/react-table';
import { differenceInDays } from 'date-fns';
import { Badge } from 'xplorer-ui';

const statusMap = {
  Pending: <Badge className="bg-primary">Pending</Badge>,
  Approved: <Badge className="bg-green-500 hover:bg-green-600">Approved</Badge>,
  Declined: <Badge variant="destructive">Declined</Badge>,
};

export const columns: ColumnDef<EmployeeLeave>[] = [
  {
    accessorKey: 'employeeName',
    header: ({ column }) => <DataTableColumnHeader column={column} title="Employee" />,
    enableSorting: false,
  },
  {
    accessorKey: 'days',
    header: ({ column }) => <DataTableColumnHeader column={column} title="Days" />,
    cell: ({ row }) => {
      return (
        <div className="flex space-x-2">
          <span className="max-w-[500px] truncate font-medium">{differenceInDays(row.original['to'], row.original['from']) + 1}</span>
        </div>
      );
    },
    enableSorting: false,
  },
  {
    accessorKey: 'from',
    header: ({ column }) => <DataTableColumnHeader column={column} title="From" />,
    cell: ({ row }) => row.original['from'].asDateOnly().toDayString(),
    enableSorting: false,
  },
  {
    accessorKey: 'to',
    header: ({ column }) => <DataTableColumnHeader column={column} title="To" />,
    cell: ({ row }) => row.original['to'].asDateOnly().toDayString(),
    enableSorting: false,
  },
  {
    accessorKey: 'status',
    header: ({ column }) => <DataTableColumnHeader column={column} title="Status" />,
    cell: ({ row }) => {
      return (
        <div className="flex space-x-2">
          <span className="max-w-[500px] truncate font-medium">{statusMap[row.original['status']]}</span>
        </div>
      );
    },
    enableSorting: false,
  },
  {
    accessorKey: 'remarks',
    header: ({ column }) => <DataTableColumnHeader column={column} title="Remarks" />,
    enableSorting: false,
  },
];
