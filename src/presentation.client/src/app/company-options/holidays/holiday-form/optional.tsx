import { Control } from 'react-hook-form';
import {
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  Switch,
} from 'xplorer-ui';
import { FormSchemaType } from './schema';

const Optional = ({ control }: { control: Control<FormSchemaType> }) => {
  return (
    <FormField
      control={control}
      name="optional"
      render={({ field }) => (
        <FormItem className="flex flex-row items-center justify-between rounded-lg border p-3 shadow-sm">
          <div className="space-y-0.5">
            <FormLabel>Optional Holiday</FormLabel>
            <FormDescription>
              If optional, employees have freedom to take this leave
            </FormDescription>
          </div>
          <FormControl>
            <Switch checked={field.value} onCheckedChange={field.onChange} />
          </FormControl>
        </FormItem>
      )}
    />
  );
};

export default Optional;
