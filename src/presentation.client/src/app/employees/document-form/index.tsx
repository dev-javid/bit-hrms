import { Employee, EmployeeDocument } from '@/lib/types';
import useFormMethods from './use-form-methods';
import { Form, FormLabel } from 'xplorer-ui';
import { FileUploadInput, FormButtons, SimpleSelect } from 'xplorer-ui';

const DocumentForm = ({
  employee,
  document,
}: {
  employee: Employee;
  document?: EmployeeDocument;
}) => {
  const { form, onSubmit } = useFormMethods({
    employeeId: employee.employeeId,
    document: document?.url ?? '',
    type: document?.type ?? 'PAN',
  });

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-2">
        <div className="grid gap-4 gap-x-8 grid-cols-1 lg:grid-cols-2">
          <div className="lg: col-span-1 space-y-6">
            <SimpleSelect
              control={form.control}
              label="Document Type"
              name="type"
              disabled={!!document}
              defaultValue={document?.type ?? ''}
              options={['PAN', 'Aadhar'].map((x) => ({
                label: x,
                value: x,
              }))}
            />
            <FileUploadInput
              control={form.control}
              label="Document"
              name="document"
              placeholder="Document"
            />
          </div>
          <div className="lg:col-span-1">
            {document && (
              <div className="space-y-6">
                <FormLabel>Existing Document</FormLabel>
                <img
                  src={document.url}
                  alt="Image"
                  className="rounded-md object-cover"
                />
              </div>
            )}
          </div>
        </div>
        <div className="pt-2 pb-4">
          <FormButtons form={form} hideCancel />
        </div>
      </form>
    </Form>
  );
};

export default DocumentForm;
