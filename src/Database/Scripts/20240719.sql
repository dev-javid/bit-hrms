CREATE TABLE salaries (
  id                SERIAL      PRIMARY KEY,
  company_id                    INTEGER      NOT NULL,
  employee_id                   INTEGER      NOT NULL,
  month                         INTEGER      NOT NULL,
  amount                        BYTEA        NOT NULL,
  created_by                    INTEGER      NOT NULL,
  created_on                    TIMESTAMPTZ  NOT NULL,
  modified_by                   INTEGER      NULL,
  modified_on                   TIMESTAMPTZ      NULL,

  CONSTRAINT fk_salaries_employee_id    FOREIGN KEY (employee_id) REFERENCES employees (id)   MATCH SIMPLE,
  CONSTRAINT fk_salaries_company_id     FOREIGN KEY (company_id)  REFERENCES companies (id)   MATCH SIMPLE
);

CREATE TABLE salary_deductions (
  id                SERIAL      PRIMARY KEY,
  company_id                    INTEGER      NOT NULL,
  salary_id                     INTEGER      NOT NULL,
  deduction_type                VARCHAR(20)  NOT NULL,
  amount                        BYTEA        NOT NULL,
  created_by                    INT          NOT NULL,
  created_on                    TIMESTAMPTZ  NOT NULL,
  modified_by                   INT              NULL,
  modified_on                   TIMESTAMPTZ      NULL,

  CONSTRAINT fk_salary_deductions_company_id               FOREIGN KEY (company_id)               REFERENCES companies               (id) MATCH SIMPLE,
  CONSTRAINT fk_salary_deductions_salary_id                FOREIGN KEY (salary_id)                REFERENCES salaries                (id) MATCH SIMPLE,
  CONSTRAINT ck_salary_deductions_type CHECK (deduction_type IN ('Absent', 'TDS'))
);
