import { Link } from 'react-router-dom';
import { Card, CardHeader, CardTitle, CardDescription, CardContent, Button, Form, TextInput } from 'xplorer-ui';
import useSubmitForm from './use-form-methods';

export default function LoginForm() {
  const { form, onSubmit, isLoading } = useSubmitForm();

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)}>
        <Card className="mx-auto max-w-sm">
          <CardHeader>
            <CardTitle className="text-2xl">Forgot Password</CardTitle>
            <CardDescription>Enter your email to reset your password, we will send you an email with a link to reset your password.</CardDescription>
          </CardHeader>
          <CardContent>
            <div className="grid gap-4">
              <TextInput control={form.control} name="email" placeholder="email" label="Email" />
              <div className="grid gap-2">
                <div className="flex items-center">
                  <Link to="sign-in" className="ml-auto inline-block text-sm underline">
                    Back to login
                  </Link>
                </div>
              </div>
              <Button type="submit" className="w-full" disabled={isLoading}>
                {!isLoading ? 'Get Link' : 'Sending Email...'}
              </Button>
            </div>
          </CardContent>
        </Card>
      </form>
    </Form>
  );
}
