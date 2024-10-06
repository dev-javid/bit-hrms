import { PlusCircle } from 'lucide-react';
import { Link } from 'react-router-dom';
import { Button, Tooltip, TooltipContent, TooltipTrigger } from 'xplorer-ui';

type Props = {
  to?: string;
  onClick?: () => void;
  tooltip?: string;
};

const AddButton = ({ to, onClick, tooltip }: Props) => {
  return (
    <Tooltip>
      <TooltipTrigger asChild>
        {to ? (
          <Link to={to}>
            <Button variant="link">
              <PlusCircle className="mr-2 h-4 w-4" />
              Add new
            </Button>
          </Link>
        ) : (
          <Button onClick={onClick} variant="outline">
            <PlusCircle className="mr-2 h-4 w-4" />
            Add new
          </Button>
        )}
      </TooltipTrigger>
      <TooltipContent>
        <p>{tooltip ?? ' Add new'}</p>
      </TooltipContent>
    </Tooltip>
  );
};

export default AddButton;
