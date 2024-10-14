import { z } from 'zod';

const nameLength = 3;
const descriptionLength = 10;

const nonNumericString = z
  .string()
  .min(nameLength, `Name is required & must be at least ${nameLength} characters long`)
  .max(50)
  .refine((value) => !/\d/.test(value), {
    message: 'Numbers are not allowed in this field',
  });

const FormSchema = z.object({
  incomeSourceId: z.number().optional(),
  name: nonNumericString,
  description: z
    .string()
    .min(descriptionLength, `Description is required & must be at least ${descriptionLength} characters long`)
    .max(500)
    .refine((value) => !/\d/.test(value), {
      message: 'Numbers are not allowed in this field',
    }),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
export default FormSchema;
