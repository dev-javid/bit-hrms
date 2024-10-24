import { z } from 'zod';

const PAN_REGEX = /^[A-Za-z]{5}\d{4}[A-Za-z]$/;
const AADHAAR_REGEX = /^\d{12}$/;

export const FormSchema = z.object({
  employeeId: z.coerce.number().optional(),
  departmentId: z.coerce.number().min(1, 'Department is required'),
  jobTitleId: z.coerce.number().min(1, 'Job Title is required'),
  firstName: z.string().min(1, { message: 'First Name is required' }).max(50),
  lastName: z.string().min(1, { message: 'Last Name is required' }).max(50),
  fullName: z.string().optional(),
  dateOfBirth: z.coerce.date(),
  dateOfJoining: z.date({
    invalid_type_error: 'Date of joining is required',
  }),
  fatherName: z.string().min(1, { message: "Father's Name is required" }).max(50),
  phoneNumber: z.string().min(1, { message: 'Phone Number is required' }).max(50),
  companyEmail: z.string().min(1, { message: 'Company Email is required' }).max(50),
  personalEmail: z.string().min(1, { message: 'Personal Email is required' }).max(50),
  address: z.string().min(1, { message: 'Address is required' }).max(50),
  city: z.string().min(1, { message: 'City is required' }).max(50),
  pan: z
    .string()
    .min(1, { message: 'PAN is required' })
    .refine((value) => PAN_REGEX.test(value), {
      message: 'Invalid PAN format',
    }),
  aadhar: z
    .string()
    .min(1, { message: 'Aadhar is required' })
    .refine((value) => AADHAAR_REGEX.test(value), {
      message: 'Invalid Aadhaar format',
    }),
});

export type FormSchemaType = z.infer<typeof FormSchema>;
