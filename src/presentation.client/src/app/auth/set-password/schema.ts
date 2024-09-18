import { z } from 'zod';

const FormSchema = z.object({
  token: z.string().min(1, 'required'),
  userId: z.string().min(1, 'required'),
  password: z.string().min(1, 'Please enter password'),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
