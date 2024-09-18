import { z } from 'zod';

const FormSchema = z.object({
  jobTitleId: z.coerce.number().optional(),
  departmentId: z.coerce.number().min(1, 'Department is required'),
  name: z.string().min(1, 'Name is required').max(50),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
