import useAuth from '@/lib/hooks/use-auth';
import routes from '@/lib/routes';
import { clearError } from '@/lib/store/slice';
import { useAppDispatch, useAppSelector } from '@/lib/store/hooks';
import { useEffect } from 'react';
import { useRoutes } from 'react-router-dom';
import { useToast } from 'xplorer-ui';
import { SessionRestoreLoader } from '@/lib/components';

function App() {
  const { toast } = useToast();
  const dispatch = useAppDispatch();
  const { errorMessage, refreshingToken } = useAppSelector((state) => state.slice);

  useEffect(() => {
    if (errorMessage) {
      toast({
        variant: 'destructive',
        title: 'Uh oh! Something went wrong.',
        description: errorMessage,
      });
      dispatch(clearError());
    }
  }, [errorMessage, dispatch, toast]);

  const { user } = useAuth();
  const pages = useRoutes(routes(!!user.id));
  return <>{refreshingToken ? <SessionRestoreLoader /> : pages}</>;
}

export default App;
