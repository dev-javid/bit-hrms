import { z } from 'zod';

const FormSchema = z.object({
  employeeId: z.coerce.number().min(1, 'Amount is required'),
  amount: z.coerce.number().min(1, 'Amount is required'),
  effectiveFrom: z.date().min(new Date(), 'Effective From is required'),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
