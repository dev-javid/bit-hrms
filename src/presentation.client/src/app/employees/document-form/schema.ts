import { z } from 'zod';

export const FormSchema = z.object({
  employeeId: z.number(),
  type: z.string().refine((value) => value === 'PAN' || value === 'Aadhar', {
    message: "Document must be either 'PAN' or 'Aadhar'",
  }),
  document: z.string().min(1, 'Please select a document'),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
