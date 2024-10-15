import { z } from 'zod';
const reasonLength = 50;

const FormSchema = z.object({
  clockInTime: z.string().min(1, 'Time is required').max(5),
  clockOutTime: z.string().min(1, 'Time is required').max(5),
  date: z.string(),
  reason: z.string().min(reasonLength, `Reason is required & must be at least ${reasonLength} characters long`).max(100),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
