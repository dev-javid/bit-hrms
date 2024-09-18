export interface Compensation {
  effectiveFrom: string;
  amount: number;
  active: boolean;
}

export interface Salary {
  salaryId: number;
  employeeId: number;
  amount: number;
  compensation: number;
  month: number;
  year: number;
  employeeName: string;
  disbursedOn?: string;
  salaryDudections: SalaryDudection[];
}

export interface SalaryDudection {
  salarayDeductionId: number;
  deductionType: string;
  deductionDate: string;
  amount: number;
}

export interface EstimatedSalary {
  employeeId: number;
  compensation: number;
  month: number;
  amountDeducted: number;
  netAmount: number;
  deductions: SalaryDudection[];
}
