import { EmployeeLeave } from '@/lib/types';
import { AlertDialog, AlertDialogContent, AlertDialogDescription, AlertDialogHeader, AlertDialogTitle } from 'xplorer-ui';
import FormContainer from './form-container';

const DeclineLeaveForm = ({ leave, onCancelDecline }: { leave: EmployeeLeave; onCancelDecline: () => void }) => {
  return (
    <AlertDialog open>
      <AlertDialogContent>
        <AlertDialogHeader className="flex-col items-start">
          <AlertDialogTitle>Decline Leave</AlertDialogTitle>
          <AlertDialogDescription>
            <FormContainer leave={leave} onCancelClick={onCancelDecline} />
          </AlertDialogDescription>
        </AlertDialogHeader>
      </AlertDialogContent>
    </AlertDialog>
  );
};

export default DeclineLeaveForm;
