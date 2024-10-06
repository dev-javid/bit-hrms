import { ArrowUpRight } from 'lucide-react';
import { Link } from 'react-router-dom';
import { Button, Tooltip, TooltipContent, TooltipTrigger } from 'xplorer-ui';

type Props = {
  text: string;
  to?: string;
  onClick?: () => void;
  tooltip?: string;
  state?: object;
};

const ActionButton = ({ text, to, onClick, tooltip, state }: Props) => {
  return (
    <Tooltip>
      <TooltipTrigger asChild>
        {to ? (
          <Link to={to} state={state}>
            <Button variant="link">
              <ArrowUpRight className="mt-0.5 mr-2 h-4 w-4" />
              {text}
            </Button>
          </Link>
        ) : (
          <Button onClick={onClick} variant="outline">
            <ArrowUpRight className="mt-0.5 mr-2 h-4 w-4" />
            {text}
          </Button>
        )}
      </TooltipTrigger>
      <TooltipContent>
        <p>{tooltip ?? text}</p>
      </TooltipContent>
    </Tooltip>
  );
};

export default ActionButton;
