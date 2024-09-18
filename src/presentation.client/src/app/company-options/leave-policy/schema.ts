import { z } from 'zod';

const FormSchema = z.object({
  casualLeaves: z.coerce.number().min(1).max(50),
  earnedLeavesPerMonth: z.coerce.number().min(0.5).max(5),
  holidays: z.coerce.number().min(1).max(30),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
