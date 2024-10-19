import { Company } from '@/lib/types';
import { ColumnDef } from '@tanstack/react-table';
import { ActionColumn } from 'xplorer-ui';

export const getColumns = (onEditClick: (value: Company) => void, onDeleteClick: (value: Company) => void) => {
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
      accessorKey: 'address',
      header: 'Address',
    },
    {
      ...ActionColumn({ onEditClick, onDeleteClick }),
    },
  ];

  return columns;
};
