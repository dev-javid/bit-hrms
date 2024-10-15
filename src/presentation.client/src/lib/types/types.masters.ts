export interface Department {
  departmentId: number;
  name: string;
}

export interface Holiday {
  holidayId: number;
  name: string;
  date: string;
  optional: boolean;
}

export interface Company {
  companyId: number;
  name: string;
  phoneNumber: string;
  email: string;
  financialMonth: string;
  administratorName: string;
  address: string;
  createdOn: string;
}

export interface JobTitle {
  jobTitleId: number;
  departmentId: number;
  name: string;
  departmentName: number;
}
