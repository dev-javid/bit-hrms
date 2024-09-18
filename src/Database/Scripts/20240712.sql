CREATE TABLE compensations (
  id                SERIAL      PRIMARY KEY,
  company_id                    INTEGER      NOT NULL,
  employee_id                   INTEGER      NOT NULL,
  effective_from                DATE         NOT NULL,
  amount                        BYTEA        NOT NULL,
  created_by                    INT          NOT NULL,
  created_on                    TIMESTAMPTZ  NOT NULL,
  modified_by                   INT              NULL,
  modified_on                   TIMESTAMPTZ      NULL,

  CONSTRAINT fk_compensations_employee_id    FOREIGN KEY (employee_id) REFERENCES employees (id)   MATCH SIMPLE,
  CONSTRAINT fk_compensations_company_id     FOREIGN KEY (company_id)  REFERENCES companies (id)   MATCH SIMPLE
);