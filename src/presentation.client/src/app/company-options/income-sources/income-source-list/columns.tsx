import { IncomeSource } from '@/lib/types';
import { ColumnDef } from '@tanstack/react-table';
import { useMemo } from 'react';
import { ActionColumn, DataTableColumnHeader } from 'xplorer-ui';

export const useColumns = (onEditClick: (value: IncomeSource) => void): ColumnDef<IncomeSource>[] => {
  return useMemo(() => {
    return [
      {
        accessorKey: 'name',
        header: ({ column }) => <DataTableColumnHeader column={column} title="Name" />,
        cell: ({ row }) => {
          return (
            <div className="flex space-x-2">
              <span className="max-w-[500px] truncate font-medium">{row.getValue('name')}</span>
            </div>
          );
        },
      },
      {
        accessorKey: 'description',
        header: ({ column }) => <DataTableColumnHeader column={column} title="Description" />,
        cell: ({ row }) => {
          return (
            <div className="flex space-x-2">
              <span className="max-w-[500px] truncate font-medium">{row.getValue('description')}</span>
            </div>
          );
        },
      },
      {
        ...ActionColumn({ onEditClick }),
      },
    ];
  }, [onEditClick]);
};
