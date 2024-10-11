import { DataTableColumnHeader, Switch } from 'xplorer-ui';
import { Holiday } from '@/lib/types';
import { ColumnDef } from '@tanstack/react-table';
import { ActionColumn } from '@/lib/components';

export const getColumns = (onEditClick: (value: Holiday) => void): ColumnDef<Holiday>[] => {
  return [
    {
      accessorKey: 'name',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Holiday" />,
      cell: ({ row }) => {
        return (
          <div className="flex space-x-2">
            <span className="max-w-[500px] truncate font-medium">{row.getValue('name')}</span>
          </div>
        );
      },
    },
    {
      accessorKey: 'date',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Date" />,
      cell: ({ row }) => {
        return (
          <div className="flex space-x-2">
            <span className="max-w-[500px] truncate font-medium">{row.original.date.asDateOnly().toDayString()}</span>
          </div>
        );
      },
    },
    {
      accessorKey: 'optional',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Optional" />,
      cell: ({ row }) => {
        return (
          <div className="flex space-x-2">
            <Switch checked={row.getValue('optional')} disabled />
          </div>
        );
      },
    },
    {
      ...ActionColumn(onEditClick),
    },
  ];
};
