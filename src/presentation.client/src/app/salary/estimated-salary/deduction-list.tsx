import { SalaryDudection } from '@/lib/types';
import { ColumnDef } from '@tanstack/react-table';
import { DataTableColumnHeader, ClientSideDataTable } from 'xplorer-ui';

const columns: ColumnDef<SalaryDudection>[] = [
  {
    accessorKey: 'deductionType',
    header: ({ column }) => <DataTableColumnHeader column={column} title="Deduction Type" />,
  },
  {
    accessorKey: 'deductionDate',
    header: ({ column }) => <DataTableColumnHeader column={column} title="Deduction Date" />,
    cell: ({ row }) => {
      return <div className="">{row.original.deductionDate.asDateOnly().toDayString()}</div>;
    },
  },
  {
    accessorKey: 'amount',
    header: ({ column }) => <DataTableColumnHeader column={column} title="Amount" />,
    cell: ({ row }) => {
      return <div className="">{row.original.amount.toFixed(2)}</div>;
    },
  },
];

const DeductionList = ({ deductions }: { deductions: SalaryDudection[] }) => {
  return (
    <ClientSideDataTable
      data={deductions}
      columns={columns}
      serialNumbers
      paging={false}
      search={false}
      columnToggle={false}
      pageSize={deductions.length}
    />
  );
};

export default DeductionList;
