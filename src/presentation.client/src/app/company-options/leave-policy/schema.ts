import { z } from 'zod';

const FormSchema = z.object({
  casualLeaves: z.coerce.number().min(0).max(100),
  earnedLeavesPerMonth: z.coerce.number().min(0).max(10),
  holidays: z.coerce.number().min(0).max(100),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
