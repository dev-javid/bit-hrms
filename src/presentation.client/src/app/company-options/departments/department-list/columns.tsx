import { ActionColumn, DataTableColumnHeader } from 'xplorer-ui';
import { Department } from '@/lib/types';
import { ColumnDef } from '@tanstack/react-table';

export const getColumns = (onEditClick: (value: Department) => void): ColumnDef<Department>[] => {
  return [
    {
      accessorKey: 'name',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Department" />,
      cell: ({ row }) => {
        return (
          <div className="flex space-x-2">
            <span className="max-w-[500px] truncate font-medium">{row.getValue('name')}</span>
          </div>
        );
      },
    },
    {
      ...ActionColumn({ onEditClick }),
    },
  ];
};
