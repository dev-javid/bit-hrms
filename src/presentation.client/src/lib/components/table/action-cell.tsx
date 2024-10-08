import { Pencil, Trash2 } from 'lucide-react';
import { Tooltip, TooltipTrigger, TooltipContent } from 'xplorer-ui';

const ActionCell = <T,>({
  onEditClick,
  onDeleteClick,
  value,
}: {
  onEditClick?: (value: T) => void;
  onDeleteClick?: (value: T) => void;
  value: T;
}) => {
  return (
    <>
      {onEditClick && (
        <Tooltip>
          <TooltipTrigger>
            <Pencil className="text-gray-500 hover:text-foreground" size={16} onClick={() => onEditClick(value)} />
          </TooltipTrigger>
          <TooltipContent>
            <p>Edit</p>
          </TooltipContent>
        </Tooltip>
      )}
      {onDeleteClick && (
        <Tooltip>
          <TooltipTrigger>
            <Trash2 className="text-gray-500 hover:text-foreground" size={16} onClick={() => onDeleteClick(value)} />
          </TooltipTrigger>
          <TooltipContent>
            <p>Delete</p>
          </TooltipContent>
        </Tooltip>
      )}
    </>
  );
};

export default ActionCell;
