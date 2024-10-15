import { z } from 'zod';

const FormSchema = z.object({
  currentPassword: z.string().min(1, 'Current password is required').max(50, 'Password can not be more than 50 characters'),
  newPassword: z.string().min(1, 'New password is required').max(50, 'Password can not be more than 50 characters'),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
