import { Button, Form, TextInput } from 'xplorer-ui';
import useSubmitForm from './use-form-methods';
import { FormSchemaType } from './schema';
import { Link } from 'react-router-dom';

export default function FormContainer({ defaultValues, reset }: { defaultValues: FormSchemaType; reset: boolean }) {
  const { form, onSubmit, isLoading } = useSubmitForm(defaultValues, reset);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)}>
        <div className="grid gap-4">
          <TextInput control={form.control} name="token" placeholder="token" type="hidden" label="" />
          <TextInput control={form.control} name="userId" placeholder="userId" type="hidden" label="" />
          <TextInput control={form.control} name="password" type="password" placeholder="Password" label="Password" />
          <div className="grid gap-2">
            <div className="flex items-center">
              <Link to="sign-in" className="ml-auto inline-block text-sm underline">
                Go to login
              </Link>
            </div>
          </div>
          <div className="pt-2">
            <Button type="submit" className="w-full" disabled={isLoading}>
              {!isLoading ? 'Set Password' : 'Please wait...'}
            </Button>
          </div>
        </div>
      </form>
    </Form>
  );
}
