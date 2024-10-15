import { z } from 'zod';

const FormSchema = z.object({
  remarks: z.string().min(1, 'Remarks/reason is required').max(200, "Remarks/reason can't be more than 200 characters"),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
