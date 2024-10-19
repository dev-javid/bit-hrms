import useFormMethods from './use-form-methods';
import { Card, CardContent, CardHeader, Form } from 'xplorer-ui';
import { DatePicker, FormButtons, SimpleSelect, TextInput } from 'xplorer-ui';
import { Department } from '@/lib/types';
import { FormSchemaType } from './schema';
import JobTitle from './job-title';

const FormContainer = ({ defaultValues, departments }: { defaultValues: FormSchemaType; departments: Department[] }) => {
  const { form, onSubmit } = useFormMethods(defaultValues);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-2">
        <Card>
          <CardHeader>Basic Information</CardHeader>
          <CardContent className="space-y-4">
            <div className="grid gap-4 gap-x-8 grid-cols-1 xl:grid-cols-3">
              <SimpleSelect
                placeholder="Select Department"
                control={form.control}
                label="Department"
                name="departmentId"
                defaultValue={defaultValues.departmentId?.toString()}
                options={departments.map((x) => ({
                  label: x.name,
                  value: x.departmentId.toString(),
                }))}
              />
              <JobTitle form={form} defaultValue={defaultValues.jobTitleId} />
            </div>
            <div className="grid gap-4 gap-x-8 grid-cols-1 xl:grid-cols-3">
              <TextInput control={form.control} label="First Name" name="firstName" placeholder="First Name" />
              <TextInput control={form.control} label="Last Name" name="lastName" placeholder="Last Name" />
              <TextInput control={form.control} label="Father's Name" name="fatherName" placeholder="Father's Name" />
              <DatePicker control={form.control} label="Date of Birth" name="dateOfBirth" placeholder="Date of Birth" />
              <DatePicker placeholder="Date of Joining" label="Date of Joining" name="dateOfJoining" control={form.control} />
            </div>
          </CardContent>
        </Card>
        <div className="grid gap-4 md:grid-cols-3">
          <Card className="xl:col-span-2">
            <CardHeader>Contact Details</CardHeader>
            <CardContent>
              <div className="grid gap-4 gap-x-8 grid-cols-1 xl:grid-cols-3">
                <TextInput control={form.control} label="Phone Number" name="phoneNumber" placeholder="Phone Number" />
                <TextInput control={form.control} label="Company Email" name="companyEmail" placeholder="Company Email" />
                <TextInput control={form.control} label="Personal Email" name="personalEmail" placeholder="Personal Email" />
              </div>
            </CardContent>
          </Card>
          <Card>
            <CardContent className="space-y-4">
              <TextInput control={form.control} label="Address" name="address" placeholder="Address" />
              <TextInput control={form.control} label="City" name="city" placeholder="City" />
            </CardContent>
          </Card>
        </div>
        <Card>
          <CardHeader>National Identity</CardHeader>
          <CardContent>
            <div className="grid gap-4 gap-x-8 grid-cols-1 xl:grid-cols-3 ">
              <TextInput control={form.control} label="PAN" name="pan" placeholder="PAN" />
              <TextInput control={form.control} label="Aadhar" name="aadhar" placeholder="Aadhar" />
            </div>
          </CardContent>
        </Card>
        <div className="pt-2 pb-4">
          <FormButtons form={form} loading={form.formState.isSubmitting} />
        </div>
      </form>
    </Form>
  );
};

export default FormContainer;
