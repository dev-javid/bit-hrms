import { Control } from 'react-hook-form';
import { FormControl, FormField, FormItem, FormLabel, FormMessage, Input } from 'xplorer-ui';
import { FormSchemaType } from './schema';

const Name = ({ control }: { control: Control<FormSchemaType> }) => {
  return (
    <FormField
      control={control}
      name="name"
      render={({ field }) => (
        <FormItem>
          <FormLabel>Holiday Name</FormLabel>
          <FormControl>
            <Input className="w-full" placeholder="name" {...field} />
          </FormControl>
          <FormMessage />
        </FormItem>
      )}
    />
  );
};

export default Name;
