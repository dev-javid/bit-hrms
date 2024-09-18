import { Spinner } from 'xplorer-ui';

const SessionRestoreLoader = () => {
  return (
    <div className="flex flex-col items-center justify-center h-screen">
      <Spinner className="text-primary" />
      <div>Restoring session...</div>
    </div>
  );
};

export default SessionRestoreLoader;
