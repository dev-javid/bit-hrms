import { Employee, EmployeeDocument } from '@/lib/types';
import useFormMethods from './use-form-methods';
import { Form } from 'xplorer-ui';
import { FileUploadInput, FormButtons } from 'xplorer-ui';

const DocumentForm = ({ employee, document, onSuccess }: { employee: Employee; document: EmployeeDocument; onSuccess: () => void }) => {
  const { form, onSubmit } = useFormMethods(
    {
      employeeId: employee.employeeId,
      document: '',
      type: document?.type,
    },
    onSuccess
  );

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-2">
        <div>
          <FileUploadInput control={form.control} label="Document" name="document" placeholder="Document" />
        </div>
        <div className="space-y-2">
          <FormButtons form={form} hideCancel />
        </div>
      </form>
    </Form>
  );
};

export default DocumentForm;
