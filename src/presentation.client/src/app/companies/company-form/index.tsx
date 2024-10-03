import useFormMethods from './use-form-methods';
import { Alert, AlertDescription, AlertTitle, Form, FormButtons, TextInput } from 'xplorer-ui';
import { Company } from '@/lib/types';
import useDefaultValues from './use-default-values';
import { useState } from 'react';

const CompanyForm = ({ company, onSuccess }: { company?: Company; onSuccess: () => void }) => {
  const [added, setAdded] = useState(false);
  const { defaultValues } = useDefaultValues(company);
  const { form, onSubmit } = useFormMethods(defaultValues, () => (company?.companyId ? onSuccess : setAdded(true)));

  return (
    <>
      {added ? (
        <Alert>
          <AlertTitle>Company Added</AlertTitle>
          <AlertDescription>
            New company <strong>{form.getValues('name')}</strong> added.
          </AlertDescription>
          <AlertDescription>
            An email has been sent to <i>{form.getValues('email')}</i> for further instructions.
          </AlertDescription>
        </Alert>
      ) : (
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <div className="grid gap-4 grid-cols-2">
              <div className="col-span-1 space-y-4">
                <TextInput control={form.control} name="name" placeholder="name" label="Name" />
                <TextInput control={form.control} name="email" placeholder="email" label="Email" disabled={!!company?.companyId} />
              </div>
              <div className="col-span-1 space-y-4">
                <TextInput control={form.control} name="administratorName" placeholder="administrator name" label="Administrator Name" />
                <TextInput control={form.control} name="phoneNumber" placeholder="phone number" label="Phone Number" />
              </div>
              <div className="col-span-2 space-y-4">
                <TextInput control={form.control} name="address" placeholder="address" label="Address" />
              </div>
              <div className="col-span-3">
                <FormButtons form={form} hideCancel loading={form.formState.isSubmitting} />
              </div>
            </div>
          </form>
        </Form>
      )}
    </>
  );
};

export default CompanyForm;
