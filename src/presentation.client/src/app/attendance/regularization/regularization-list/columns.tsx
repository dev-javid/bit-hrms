import { Badge, Button, DataTableColumnHeader } from 'xplorer-ui';
import { AttendanceRegularization, User } from '@/lib/types';
import { ColumnDef } from '@tanstack/react-table';
import { convertUtcTimeToLocalTime } from '@/lib/utils';

export const getColumns = (onApproveClick: (regularization: AttendanceRegularization) => void, user: User) => {
  const columns: ColumnDef<AttendanceRegularization>[] = [
    {
      accessorKey: 'date',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Date" />,
      cell: ({ row }) => {
        return (
          <div className="flex space-x-2">
            <span className="max-w-[500px] truncate font-medium">{row.original['date'].asDateOnly().toDayString()}</span>
          </div>
        );
      },
    },
    {
      accessorKey: 'Clock In',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Date" />,
      cell: ({ row }) => {
        return (
          <div className="space-x-2">
            <span className="text-blue-700 font-medium">{convertUtcTimeToLocalTime(row.original['clockInTime'])}</span>
          </div>
        );
      },
    },
    {
      accessorKey: 'Clockl Out',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Optional" />,
      cell: ({ row }) => {
        return <div className="space-x-2 text-blue-700 font-medium">{convertUtcTimeToLocalTime(row.original['clockOutTime'])}</div>;
      },
    },
    {
      accessorKey: 'reason',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Reason" />,
      cell: ({ row }) => {
        return <div className="flex space-x-2">{row.original['reason']}</div>;
      },
    },
    {
      accessorKey: 'status',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Status" />,
      cell: ({ row }) => {
        return (
          <div className="flex space-x-2">
            {row.original['approved'] ? (
              <Badge variant="outline" className="text-green-600 hover:text-green-400">
                Approved
              </Badge>
            ) : (
              <Badge variant="secondary">Pending</Badge>
            )}
          </div>
        );
      },
    },
  ];

  if (user.isCompanyAdmin) {
    columns.push({
      accessorKey: 'action',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Approve" />,
      cell: ({ row }) => {
        return (
          <div className="flex space-x-2">
            {!row.original['approved'] && (
              <Button variant="outline" onClick={() => onApproveClick(row['original'])}>
                Approve
              </Button>
            )}
          </div>
        );
      },
    });
  }

  return columns;
};
