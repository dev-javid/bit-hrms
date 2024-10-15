import { Salary } from '@/lib/types';
import { ColumnDef } from '@tanstack/react-table';
import _ from 'lodash';
import { Link } from 'react-router-dom';
import { Button, Badge, DataTableColumnHeader } from 'xplorer-ui';

export const columns: ColumnDef<Salary>[] = [
  {
    accessorKey: 'employeeName',
    header: "Employee's Name",
  },
  {
    accessorKey: 'salaryDisbursed',
    header: 'Salary Disbursed',
    cell: ({ row }) => {
      return (
        <div>
          <span>{row.original.salaryId ? <Badge>Yes</Badge> : <Badge variant="secondary">No</Badge>}</span>
        </div>
      );
    },
  },
  {
    accessorKey: 'disbursedOn',
    header: ({ column }) => <DataTableColumnHeader column={column} title="Disbursed On" />,
    cell: ({ row }) => {
      return (
        <div>
          <span>{row.original.disbursedOn?.asDateOnly()?.toDayString()}</span>
        </div>
      );
    },
  },
  {
    accessorKey: 'compensation',
    header: "Employee's Salary",
    cell: ({ row }) => {
      return (
        <div>
          <span>{row.original.compensation ? `₹${row.original.compensation.toFixed(2)}` : null}</span>
        </div>
      );
    },
  },
  {
    accessorKey: 'amount',
    header: 'Amount Paid',
    cell: ({ row }) => {
      return (
        <div>
          <span>{row.original.salaryId ? `₹${row.original.amount.toFixed(2)}` : null}</span>
        </div>
      );
    },
  },
  {
    header: 'deductions',
    cell: ({ row }) => {
      return (
        <div>
          <span>{row.original.salaryId ? `₹${_.sum(row.original.salaryDudections.map((d) => d.amount)).toFixed(2)}` : null}</span>
        </div>
      );
    },
  },
  {
    accessorKey: 'action',
    header: ({ column }) => <DataTableColumnHeader column={column} title="Action" />,
    cell: ({ row }) => {
      return (
        <div>
          <span>
            <Button asChild variant="link">
              <Link
                to="./../salary/estimated-salary"
                state={{
                  salary: row.original,
                }}
              >
                {row.original.salaryId ? 'View' : 'Pay Now'}
              </Link>
            </Button>
          </span>
        </div>
      );
    },
  },
];
