CREATE TABLE income_sources (
  id                SERIAL      PRIMARY KEY,
  company_id                    INTEGER      NOT NULL,
  name                          VARCHAR(50)  NOT NULL,
  description                   TEXT         NOT NULL,
  created_by                    INTEGER      NOT NULL,
  created_on                    TIMESTAMPTZ  NOT NULL,
  modified_by                   INTEGER          NULL,
  modified_on                   TIMESTAMPTZ      NULL,
  CONSTRAINT fk_income_sources_company_id     FOREIGN KEY (company_id)  REFERENCES companies (id)   MATCH SIMPLE
);

CREATE TABLE incomes (
  id                SERIAL      PRIMARY KEY,
  company_id                    INTEGER         NOT NULL,
  income_source_id              INTEGER         NOT NULL,
  amount                        BYTEA           NOT NULL,
  documents                     varchar(50)[]   NOT NULL,
  remarks                       TEXT                NULL,
  created_by                    INTEGER         NOT NULL,
  created_on                    TIMESTAMPTZ     NOT NULL,
  modified_by                   INTEGER             NULL,
  modified_on                   TIMESTAMPTZ         NULL,
  CONSTRAINT fk_incomes_income_source_id   FOREIGN KEY (income_source_id)  REFERENCES income_sources (id)  MATCH SIMPLE,
  CONSTRAINT fk_incomes_company_id         FOREIGN KEY (company_id)        REFERENCES companies (id)       MATCH SIMPLE
);

CREATE TABLE expenses (
  id                SERIAL      PRIMARY KEY,
  company_id                    INTEGER             NOT NULL,
  amount                        BYTEA               NOT NULL,
  documents                     varchar(50)[]       NOT NULL,
  purpose                       TEXT                    NULL,
  created_by                    INTEGER             NOT NULL,
  created_on                    TIMESTAMPTZ         NOT NULL,
  modified_by                   INTEGER                 NULL,
  modified_on                   TIMESTAMPTZ             NULL,
  CONSTRAINT fk_expenses_company_id         FOREIGN KEY (company_id)        REFERENCES companies (id)       MATCH SIMPLE
);
