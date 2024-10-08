import { z } from 'zod';

const FormSchema = z.object({
  companyId: z.number().optional(),
  name: z.string().min(1, 'Name is required').max(50),
  administratorName: z
    .string()
    .min(1, 'Company administrator name is required')
    .max(50),
  phoneNumber: z.string().min(1, 'Phone number is required').max(50),
  email: z.string().email(),
  financialMonth: z.coerce
    .number()
    .min(1, 'Financial month is required')
    .max(3),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
