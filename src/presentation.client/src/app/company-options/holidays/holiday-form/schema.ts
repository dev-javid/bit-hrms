import { z } from 'zod';

const FormSchema = z.object({
  holidayId: z.number(),
  name: z.string().min(1, 'Name is required').max(50),
  date: z.coerce.date(),
  optional: z.boolean().optional(),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
