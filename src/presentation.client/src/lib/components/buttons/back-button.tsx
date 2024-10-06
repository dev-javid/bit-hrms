import { CircleArrowLeft } from 'lucide-react';
import { Link } from 'react-router-dom';
import { Button, Tooltip, TooltipContent, TooltipTrigger } from 'xplorer-ui';

type Props = {
  to: string;
  text: string;
  state?: object;
  tooltip?: string;
};

const BackButton = ({ to, text, tooltip, state }: Props) => {
  return (
    <Tooltip>
      <TooltipTrigger asChild>
        <Link to={to}>
          <Button variant="link" state={state}>
            <CircleArrowLeft className="mt-0.5 mr-2 h-4 w-4" />
            {text}
          </Button>
        </Link>
      </TooltipTrigger>
      <TooltipContent>
        <p>{tooltip ?? `Back to ${text.toLowerCase()}`}</p>
      </TooltipContent>
    </Tooltip>
  );
};

export default BackButton;
