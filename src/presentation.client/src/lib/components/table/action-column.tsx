import { ColumnDef } from '@tanstack/react-table';
import { DataTableColumnHeader } from 'xplorer-ui';
import ActionCell from './action-cell';

export function ActionColumn<T>(onEditClick?: (value: T) => void, onDeleteClick?: (value: T) => void): ColumnDef<T> {
  const column: ColumnDef<T> = {
    accessorKey: 'actions',
    header: ({ column }) => <DataTableColumnHeader className="flex justify-end" column={column} title="Actions" />,
    enableSorting: false,
    cell: ({ row }) => (
      <div className="flex justify-end gap-2">
        <ActionCell onEditClick={onEditClick} onDeleteClick={onDeleteClick} value={row.original} />
      </div>
    ),
  };

  return column;
}
