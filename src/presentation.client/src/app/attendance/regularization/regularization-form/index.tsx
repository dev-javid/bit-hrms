import { InOutTiming } from '@/lib/types';
import FormContainer from './form-container';
import useDefaultValues from './use-default-values';
import { Alert, AlertDescription, AlertTitle } from 'xplorer-ui';
import { useState } from 'react';
import { CheckCircle } from 'lucide-react';

export default function RegularizationForm({ inoutTiming }: { inoutTiming: InOutTiming }) {
  const { defaultValues } = useDefaultValues(inoutTiming);
  const [success, setSuccess] = useState(false);

  return (
    <>
      {!success && defaultValues && <FormContainer defaultValues={defaultValues} onSuccess={() => setSuccess(true)} />}
      {success && (
        <Alert variant="primary" className="bg-primary">
          <CheckCircle className="h-4 w-4" />
          <AlertTitle>Success</AlertTitle>
          <AlertDescription>
            Your regularization request has been submitted.
            <p>It will be reviewed by the HR team.</p>
          </AlertDescription>
        </Alert>
      )}
    </>
  );
}
