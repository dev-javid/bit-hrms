import { z } from 'zod';

const FormSchema = z.object({
  companyId: z.number().optional(),
  name: z.string().min(1, 'Name is required').max(50),
  administratorName: z.string().min(1, 'Company administrator name is required').max(50),
  phoneNumber: z.string().min(1, 'Phone number is required').max(50),
  address: z.string().min(30, 'Address is required and must be at least 50 characters').max(200),
  email: z.string().email(),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
