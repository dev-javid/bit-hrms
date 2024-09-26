import { Company } from '@/lib/types';
import { ColumnDef } from '@tanstack/react-table';
import { Pencil, Trash2 } from 'lucide-react';
import { DataTableColumnHeader, Tooltip, TooltipTrigger, TooltipContent } from 'xplorer-ui';

export const useColumns = (onEditClick: (company: Company) => void, onDeleteClick: (company: Company) => void) => {
  const columns: ColumnDef<Company>[] = [
    {
      accessorKey: 'name',
      header: 'Company',
    },
    {
      accessorKey: 'administratorName',
      header: 'Administrator',
    },
    {
      accessorKey: 'phoneNumber',
      header: 'Phone Number',
      cell: ({ row }) => {
        return (
          <a
            href={`tel:${row.original.phoneNumber}`}
            className="text-gray-500 hover:text-foreground font-normal transition-colors duration-200 ease-in-out"
          >
            {row.original.phoneNumber}
          </a>
        );
      },
    },
    {
      accessorKey: 'email',
      header: 'Email',
      cell: ({ row }) => {
        return (
          <div>
            <a href={`mailto:${row.original.email}`} className="text-blue-600 hover:text-blue-800 hover:underline">
              {row.original.email}
            </a>
          </div>
        );
      },
    },
    {
      accessorKey: 'city',
      header: ({ column }) => <DataTableColumnHeader className="flex justify-end" column={column} title="Actions" />,
      enableSorting: false,
      cell: ({ row }) => (
        <div className="flex justify-end gap-2">
          <Tooltip>
            <TooltipTrigger>
              <Pencil className="text-gray-500 hover:text-foreground" size={16} onClick={() => onEditClick(row.original)} />
            </TooltipTrigger>
            <TooltipContent>
              <p>Edit</p>
            </TooltipContent>
          </Tooltip>
          <Tooltip>
            <TooltipTrigger>
              <Trash2 className="hover:text-destructive" size={16} onClick={() => onDeleteClick(row.original)} />
            </TooltipTrigger>
            <TooltipContent>
              <p>Delete</p>
            </TooltipContent>
          </Tooltip>
        </div>
      ),
    },
  ];

  return columns;
};
