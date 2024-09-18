import { useEffect } from 'react';
import { FormSchemaType } from './schema';
import useFormMethods from './use-form-methods';
import {
  Card,
  CardContent,
  Form,
  FormButtons,
  SimpleSelect,
  TextInput,
} from 'xplorer-ui';
import _ from 'lodash';

const months = {
  January: 1,
  March: 3,
};

const FormContainer = ({
  defaultValues,
}: {
  defaultValues: FormSchemaType;
}) => {
  const { form, onSubmit } = useFormMethods(defaultValues);

  useEffect(() => {
    form.reset(defaultValues);
  }, [defaultValues, form]);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)}>
        <Card>
          <CardContent>
            <div className="grid gap-4 grid-cols-1 lg:grid-cols-3">
              <div className="col-span-1 space-y-4">
                <TextInput
                  control={form.control}
                  name="name"
                  placeholder="name"
                  label="Name"
                />
                <TextInput
                  control={form.control}
                  name="email"
                  placeholder="email"
                  label="Email"
                  disabled={defaultValues.companyId != 0}
                />
              </div>
              <div className="col-span-1 space-y-4">
                <TextInput
                  control={form.control}
                  name="administratorName"
                  placeholder="administrator name"
                  label="Administrator Name"
                  disabled={defaultValues.companyId != 0}
                />
                <TextInput
                  control={form.control}
                  name="phoneNumber"
                  placeholder="phoneNumber"
                  label="Phone Number"
                  disabled={defaultValues.companyId != 0}
                />
              </div>
              <div></div>
              <SimpleSelect
                control={form.control}
                label="Financial Month"
                name="financialMonth"
                defaultValue={defaultValues.financialMonth.toString()}
                options={_.map(months, (value, key) => ({
                  label: key,
                  value: value.toString(),
                }))}
              />
              <div className="col-span-3">
                <FormButtons form={form} />
              </div>
            </div>
          </CardContent>
        </Card>
      </form>
    </Form>
  );
};

export default FormContainer;
