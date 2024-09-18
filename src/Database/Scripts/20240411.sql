﻿-- departments
CREATE TABLE public.departments (
    id              SERIAL          NOT NULL,
	company_id      INTEGER         NOT NULL,
    name            VARCHAR(50)     NOT NULL,
    created_by      INTEGER         NOT NULL,
    created_on      TIMESTAMPTZ     NOT NULL,
    modified_by     INTEGER             NULL,
    modified_on     TIMESTAMPTZ         NULL,
    CONSTRAINT pk_departments               PRIMARY KEY (id),
    CONSTRAINT fk_departments_company_id    FOREIGN KEY (company_id) REFERENCES companies (id) MATCH SIMPLE
);
CREATE UNIQUE INDEX uk_departments_company_id_name ON public.departments USING btree (company_id, name);

-- leave_policies
CREATE TABLE public.leave_policies (
    id                          SERIAL          NOT NULL,
    company_id                  INTEGER         NOT NULL,
    casual_leaves               INTEGER         NOT NULL,
    earned_leaves_per_month     NUMERIC(18,2)   NOT NULL,
    holidays                    INTEGER         NOT NULL,
    created_by                  INTEGER         NOT NULL,
    created_on                  TIMESTAMPTZ     NOT NULL,
    modified_by                 INTEGER             NULL,
    modified_on timestamptz NULL,
    CONSTRAINT pk_leave_policies                PRIMARY KEY (id),
    CONSTRAINT fk_leave_policies_company_id     FOREIGN KEY (company_id) REFERENCES companies (id) MATCH SIMPLE
);
CREATE UNIQUE INDEX uk_leave_policies_company_id ON public.leave_policies USING btree (company_id);

-- holidays
CREATE TABLE public.holidays (
    id              SERIAL          NOT NULL,
	company_id      INTEGER         NOT NULL,
    name            VARCHAR(50)     NOT NULL,
    date            DATE            NOT NULL,
    optional        BOOLEAN         NOT NULL,
    created_by      INTEGER         NOT NULL,
    created_on      TIMESTAMPTZ     NOT NULL,
    modified_by     INTEGER             NULL,
    modified_on     TIMESTAMPTZ         NULL,
    CONSTRAINT pk_holidays              PRIMARY KEY (id),
    CONSTRAINT fk_holidays_company_id   FOREIGN KEY (company_id) REFERENCES companies (id) MATCH SIMPLE
);
CREATE UNIQUE INDEX uk_holidays_company_id_name_date ON public.holidays USING btree (company_id, name, date);


-- job-titles
CREATE TABLE job_titles (
    id              SERIAL      NOT NULL,
	company_id      INTEGER     NOT NULL,
    department_id   INTEGER     NOT NULL,
    name            VARCHAR     NOT NULL,
    created_by      INTEGER     NOT NULL,
    created_on      TIMESTAMPTZ NOT NULL,
    modified_by     INTEGER         NULL,
    modified_on     TIMESTAMPTZ     NULL,
    CONSTRAINT pk_job_titles PRIMARY KEY (id),
    CONSTRAINT fk_job_titles_company_id     FOREIGN KEY (company_id)        REFERENCES companies (id)       MATCH SIMPLE,
    CONSTRAINT fk_job_titles_department_id  FOREIGN KEY  (department_id)    REFERENCES departments (id)     MATCH SIMPLE
);
CREATE UNIQUE INDEX uk_job_titles_name_company_id_department_id ON public.job_titles USING btree (name, company_id, department_id);


-- employees
CREATE TABLE employees (
    id              SERIAL      NOT NULL,
	company_id      INTEGER     NOT NULL,
    user_id         INTEGER     NOT NULL,
    department_id   INTEGER     NOT NULL,
    job_title_id    INTEGER     NOT NULL,
    first_name      VARCHAR     NOT NULL,
    last_name       VARCHAR     NOT NULL,
    date_of_birth   DATE        NOT NULL,
    date_of_joining DATE        NOT NULL,
    father_name     VARCHAR     NOT NULL,
    phone_number    VARCHAR     NOT NULL,
    company_email   VARCHAR     NOT NULL,
    personal_email  VARCHAR     NOT NULL,
    address         VARCHAR     NOT NULL,
    city            VARCHAR     NOT NULL,
    pan             VARCHAR     NOT NULL,
    aadhar          VARCHAR     NOT NULL,
    created_by      INTEGER     NOT NULL,
    created_on      TIMESTAMPTZ NOT NULL,
    modified_by     INTEGER         NULL,
    modified_on     TIMESTAMPTZ     NULL,
    CONSTRAINT pk_employees PRIMARY KEY (id),
    CONSTRAINT fk_employees_company_id      FOREIGN KEY (company_id)    REFERENCES companies (id)   MATCH SIMPLE,
    CONSTRAINT fk_employees_department_id   FOREIGN KEY (department_id) REFERENCES departments (id) MATCH SIMPLE,
    CONSTRAINT fk_employees_job_title_id    FOREIGN KEY (job_title_id)  REFERENCES job_titles (id)  MATCH SIMPLE,
    CONSTRAINT fk_employees_user_id         FOREIGN KEY (user_id)       REFERENCES users (id)       MATCH SIMPLE
);
CREATE UNIQUE INDEX uk_employees_company_id_company_email ON public.employees USING btree (company_id, company_email);


-- employee_documents
CREATE TABLE employee_documents (
    id              SERIAL      NOT NULL,
	company_id      INTEGER     NOT NULL,
    employee_id     INTEGER     NOT NULL,
    document_type   VARCHAR     NOT NULL,
    file_name       VARCHAR     NOT NULL,
    created_by      INTEGER     NOT NULL,
    created_on      TIMESTAMPTZ NOT NULL,
    modified_by     INTEGER         NULL,
    modified_on     TIMESTAMPTZ     NULL,
    CONSTRAINT pk_employee_documents PRIMARY KEY (id),
    CONSTRAINT fk_employee_documents_company_id     FOREIGN KEY (company_id)    REFERENCES companies (id)   MATCH SIMPLE,
    CONSTRAINT fk_employee_documents_employee_id    FOREIGN KEY  (employee_id) REFERENCES employees (id)    MATCH SIMPLE
);
CREATE UNIQUE INDEX uk_employee_documents_employee_id_document_type ON public.employee_documents USING btree (employee_id, document_type);


-- employee_documents
CREATE TABLE employee_leaves (
    id              SERIAL      NOT NULL,
	company_id      INTEGER     NOT NULL,
    employee_id     INTEGER     NOT NULL,
    from_date       DATE        NOT NULL,
    to_date         DATE        NOT NULL,
    status          VARCHAR     NOT NULL,
    remarks         TEXT            NULL,
    created_by      INTEGER     NOT NULL,
    created_on      TIMESTAMPTZ NOT NULL,
    modified_by     INTEGER         NULL,
    modified_on     TIMESTAMPTZ     NULL,
    CONSTRAINT pk_employee_leaves PRIMARY KEY (id),
    CONSTRAINT fk_employee_leaves_company_id     FOREIGN KEY (company_id)   REFERENCES companies (id)   MATCH SIMPLE,
    CONSTRAINT fk_employee_leaves_employee_id    FOREIGN KEY (employee_id)  REFERENCES employees (id)   MATCH SIMPLE
);
