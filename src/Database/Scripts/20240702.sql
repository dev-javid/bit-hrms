CREATE TABLE attendance_regularization (
  id                SERIAL      PRIMARY KEY,
  company_id                    INTEGER      NOT NULL,
  employee_id                   INTEGER      NOT NULL,
  date                          DATE         NOT NULL,
  clock_in_time                 TIME         NOT NULL,
  clock_out_time                TIME         NOT NULL,
  reason                        VARCHAR(255) NOT NULL,
  approved                      bool         NOT NULL,
  created_by                    INT          NOT NULL,
  created_on                    TIMESTAMPTZ  NOT NULL,
  modified_by                   INT              NULL,
  modified_on                   TIMESTAMPTZ      NULL,

  UNIQUE (employee_id, date),
  CONSTRAINT fk_attendance_regularization_employee_id    FOREIGN KEY (employee_id) REFERENCES employees (id)   MATCH SIMPLE,
  CONSTRAINT fk_attendance_regularization_company_id     FOREIGN KEY (company_id)  REFERENCES companies (id)   MATCH SIMPLE
);