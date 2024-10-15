import { DataTableColumnHeader, Tooltip, TooltipContent, TooltipTrigger } from 'xplorer-ui';
import { InOutTiming } from '@/lib/types';
import { convertUtcTimeToLocalTime, timeDifference } from '@/lib/utils';
import { ColumnDef } from '@tanstack/react-table';
import { Badge } from 'xplorer-ui';
import { InfoIcon } from 'lucide-react';
import { ActionButton } from '@/lib/components';

const statusMap = {
  ClockedIn: <Badge variant="outline">Clocked In</Badge>,
  WeeklyOff: <Badge variant="secondary">Weekly Off</Badge>,
  Holiday: <Badge variant="secondary">Holiday</Badge>,
  OnLeave: <Badge variant="secondary">On Leave</Badge>,
  Approved: (
    <Badge variant="outline" className="text-green-600 hover:text-green-400">
      Approved
    </Badge>
  ),
  ClockInMissing: (
    <Badge variant="outline" className="text-red-600 hover:text-red-400">
      Clock In Missing
    </Badge>
  ),
  ClockOutMissing: (
    <Badge variant="outline" className="text-red-600 hover:text-red-400">
      Clock Out Missing
    </Badge>
  ),
  RegularizationRequested: <Badge variant="outline">Awaiting Approval</Badge>,
};

export const getColumns = (onRegularizeClick: (inoutTiming: InOutTiming) => void) => {
  const columns: ColumnDef<InOutTiming>[] = [
    {
      accessorKey: 'date',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Date" />,
      enableSorting: false,
      cell: ({ row }) => {
        return <div className="flex space-x-2">{row.original['date'].asDateOnly().toDayString()}</div>;
      },
    },
    {
      accessorKey: 'Hours',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Hours" />,
      cell: ({ row }) => {
        return (
          <div className="">
            {row.original['status'] == 'RegularizationRequested' ? (
              <div className="flex space-x-2">
                {timeDifference(row.original['regularization']!.clockInTime, row.original['regularization']!.clockOutTime)}
                <RegularizationIcon tooltip="Calculated based on regularization." reason={''} />
              </div>
            ) : (
              <>
                {row.original['clockInTime'] && row.original['clockOutTime']
                  ? timeDifference(row.original['clockInTime'], row.original['clockOutTime'])
                  : ''}
              </>
            )}
          </div>
        );
      },
    },
    {
      accessorKey: 'Clock in',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Clock In" />,
      cell: ({ row }) => {
        return row.original['status'] == 'RegularizationRequested' && !row.original['clockInTime'] ? (
          <div className="flex space-x-2 text-blue-700 font-medium">
            {convertUtcTimeToLocalTime(row.original['regularization']!.clockInTime)}
            <RegularizationIcon tooltip="Missed clock in." reason={row.original['regularization']!.reason} />
          </div>
        ) : (
          <div className="text-blue-700 font-medium">{row.original['clockInTime'] ? convertUtcTimeToLocalTime(row.original['clockInTime']) : ''}</div>
        );
      },
    },
    {
      accessorKey: 'Clock Out',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Clock Out" />,
      cell: ({ row }) => {
        return (
          <div className="text-blue-700 font-medium">
            {row.original['status'] == 'RegularizationRequested' ? (
              <div className="flex space-x-2">
                {convertUtcTimeToLocalTime(row.original['regularization']!.clockOutTime)}
                <RegularizationIcon tooltip="Missed clock out." reason={row.original['regularization']!.reason} />
              </div>
            ) : (
              <>{row.original['clockOutTime'] ? convertUtcTimeToLocalTime(row.original['clockOutTime']) : ''}</>
            )}
          </div>
        );
      },
    },
    {
      accessorKey: 'Status',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Status" />,
      cell: ({ row }) => {
        return <div className="flex space-x-2">{statusMap[row.original['status']]}</div>;
      },
    },
    {
      accessorKey: 'Action',
      header: ({ column }) => <DataTableColumnHeader column={column} title="Action" />,
      cell: ({ row }) => {
        return (
          <div className="flex space-x-2">
            {row.original['status'] == 'ClockInMissing' || row.original['status'] == 'ClockOutMissing' ? (
              <ActionButton text="Regularize" onClick={() => onRegularizeClick(row.original)} tooltip="Regularize attendance" />
            ) : null}
          </div>
        );
      },
    },
  ];

  return columns;
};

const RegularizationIcon = ({ tooltip, reason }: { tooltip: string; reason: string }) => {
  return (
    <Tooltip>
      <TooltipTrigger asChild>
        <InfoIcon className="m-1 text-yellow-600" size={14} />
      </TooltipTrigger>
      <TooltipContent className="bg-secondary">
        <p>{tooltip}</p>
        {reason && (
          <div className="pt-3">
            Reason:
            <p className="text-xs">{reason}</p>
          </div>
        )}
      </TooltipContent>
    </Tooltip>
  );
};
