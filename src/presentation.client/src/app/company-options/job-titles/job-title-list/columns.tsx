import { ActionColumn, DataTableColumnHeader } from 'xplorer-ui';
import { JobTitle } from '@/lib/types';
import { ColumnDef } from '@tanstack/react-table';

export const getColumns = (onEditClick: (value: JobTitle) => void) => {
  return [
    {
      accessorKey: 'name',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Job title" />,
      cell: ({ row }) => {
        return (
          <div className="flex space-x-2">
            <span className="max-w-[500px] truncate font-medium">{row.getValue('name')}</span>
          </div>
        );
      },
    },
    {
      accessorKey: 'departmentName',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Department" />,
      cell: ({ row }) => {
        return (
          <div className="flex space-x-2">
            <span className="max-w-[500px] truncate font-medium">{row.getValue('departmentName')}</span>
          </div>
        );
      },
    },
    {
      ...ActionColumn({ onEditClick }),
    },
  ] as ColumnDef<JobTitle>[];
};
