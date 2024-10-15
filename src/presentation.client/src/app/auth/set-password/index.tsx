import FormContainer from './form-container';
import { Card, CardHeader, CardTitle, CardDescription, CardContent } from 'xplorer-ui';
import useDefaultValues from './use-default-values';
import { useEffect } from 'react';
import { deleteAauthTokens } from '@/lib/types';

export default function SetPassword({ reset }: { reset: boolean }) {
  const { defaultValues } = useDefaultValues();

  useEffect(() => {
    deleteAauthTokens();
  }, []);

  return (
    <div className="pt-10">
      <Card className="mx-auto max-w-sm">
        <CardHeader>
          <CardTitle className="text-2xl">{reset ? 'Reset Password' : 'Set Password'}</CardTitle>
          <CardDescription>{reset ? 'Please enter your new password.' : 'Please create a password for your account.'}</CardDescription>
        </CardHeader>
        <CardContent>{defaultValues.token && <FormContainer defaultValues={defaultValues} reset={reset} />}</CardContent>
      </Card>
    </div>
  );
}
