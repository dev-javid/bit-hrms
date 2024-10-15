import { z } from 'zod';

const FormSchema = z.object({
  departmentId: z.number(),
  name: z.string().min(1, 'Name is required').max(50, "Name can't be more than 50 characters"),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
