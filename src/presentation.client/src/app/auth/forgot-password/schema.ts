import { z } from 'zod';

const FormSchema = z.object({
  email: z.string().min(1, 'Email is required').email(),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
