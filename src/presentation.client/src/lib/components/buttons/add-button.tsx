import { PlusCircle } from 'lucide-react';
import { Link } from 'react-router-dom';
import { Button, Tooltip, TooltipContent, TooltipTrigger } from 'xplorer-ui';

type Props = {
  to?: string;
  onClick?: () => void;
  text?: string;
  tooltip?: string;
  className?: string;
  state?: object;
};

const AddButton = ({ to, onClick, tooltip, state, text = 'Add new', className }: Props) => {
  return (
    <Tooltip>
      <TooltipTrigger asChild>
        {to ? (
          <Link to={to} state={state}>
            <Button variant="link" className={className}>
              <PlusCircle className="mr-2 h-4 w-4" />
              {text}
            </Button>
          </Link>
        ) : (
          <Button onClick={onClick} variant="outline">
            <PlusCircle className="mr-2 h-4 w-4" />
            {text}
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
