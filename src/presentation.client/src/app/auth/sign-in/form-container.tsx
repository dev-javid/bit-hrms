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
            <CardTitle className="text-2xl">Login</CardTitle>
            <CardDescription>Enter your email below to login to your account</CardDescription>
          </CardHeader>
          <CardContent>
            <div className="grid gap-4">
              <TextInput control={form.control} name="email" placeholder="email" label="Email" />
              <TextInput control={form.control} name="password" type="password" placeholder="password" label="Password" />
              <div className="grid gap-2">
                <div className="flex items-center">
                  <Link to="forgot-password" className="ml-auto inline-block text-sm underline">
                    Forgot your password?
                  </Link>
                </div>
              </div>
              <Button type="submit" className="w-full" disabled={isLoading}>
                {!isLoading ? 'Login' : 'Signin in...'}
              </Button>
            </div>
          </CardContent>
        </Card>
      </form>
    </Form>
  );
}
