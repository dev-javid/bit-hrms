import useFormMethods from './use-form-methods';
import { Form } from 'xplorer-ui';
import { FormButtons, TextInput } from 'xplorer-ui';

const ChangePasswordForm = () => {
  const { form, onSubmit } = useFormMethods();

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className=" space-y-6">
        <TextInput name="currentPassword" label="Current Password" placeholder="Current Password" control={form.control} type="password" />
        <TextInput name="newPassword" label="New Password" placeholder="New Password" control={form.control} type="password" />
        <FormButtons form={form} />
      </form>
    </Form>
  );
};

export default ChangePasswordForm;
