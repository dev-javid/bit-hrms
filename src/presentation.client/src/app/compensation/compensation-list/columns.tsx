import { DataTableColumnHeader } from 'xplorer-ui';
import { Compensation } from '@/lib/types';
import { ColumnDef } from '@tanstack/react-table';
import { Badge } from 'xplorer-ui';

export const columns: ColumnDef<Compensation>[] = [
  {
    accessorKey: 'effectiveFrom',
    header: ({ column }) => <DataTableColumnHeader column={column} title="Effective From" />,
    cell: ({ row }) => {
      return (
        <div className="flex space-x-2">
          <span className="max-w-[500px] truncate font-medium">{row.original['effectiveFrom'].asDateOnly().toDayString()}</span>
        </div>
      );
    },
  },
  {
    accessorKey: 'amount',
    header: ({ column }) => <DataTableColumnHeader column={column} title="Amount" />,
    cell: ({ row }) => {
      return (
        <div className="flex space-x-2">
          <span className="max-w-[500px] truncate font-medium">{row.getValue('amount')}</span>
        </div>
      );
    },
  },
  {
    accessorKey: 'active',
    header: ({ column }) => <DataTableColumnHeader column={column} title="Active" />,
    cell: ({ row }) => {
      return (
        <div className="flex space-x-2">
          <span className="max-w-[500px] truncate font-medium">
            {row.getValue('active') ? <Badge>Yes</Badge> : <Badge variant="secondary">No</Badge>}
          </span>
        </div>
      );
    },
  },
];
