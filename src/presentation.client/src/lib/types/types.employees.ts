export interface Employee {
  employeeId: number;
  departmentId: number;
  departmentName: string;
  firstName: string;
  lastName: string;
  fullName: string;
  dateOfBirth: string;
  dateOfJoining: string;
  fatherName: string;
  jobTitleId: number;
  jobTitleName: string;
  phoneNumber: string;
  companyEmail: string;
  personalEmail: string;
  address: string;
  city: string;
  pan: string;
  aadhar: string;
  compensation: number;
  documents: EmployeeDocument[];
}

export interface EmployeeLeave {
  employeeLeaveId: number;
  employeeId: number;
  employeeName: string;
  from: string;
  to: string;
  status: 'Pending' | 'Approved' | 'Declined';
  string: string;
}

export interface EmployeeDocument {
  type: DocumentType;
  url: string;
  updatedOn: Date;
}

export type DocumentType = 'PAN' | 'Aadhar';
