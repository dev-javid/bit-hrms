--clock_in_out_times
CREATE TABLE public.clock_in_out_times (
    id                  SERIAL          NOT NULL,
    employee_id         INT             NOT NULL,
    company_id          INT             NOT NULL,
    date                DATE            NOT NULL,
    clock_in_time       Time            NOT NULL,
    clock_out_time      Time                NULL,
    created_by          INT             NOT NULL,
    created_on          TIMESTAMPTZ     NOT NULL,
    modified_by         INT                 NULL,
    modified_on         TIMESTAMPTZ         NULL,
    CONSTRAINT          pk_clock_in_out_times             PRIMARY KEY (id),
    CONSTRAINT          fk_clock_in_out_times_employee_id FOREIGN KEY (employee_id) REFERENCES employees (id) MATCH SIMPLE,
    CONSTRAINT          fk_clock_in_out_times_company_id  FOREIGN KEY (company_id)  REFERENCES companies (id) MATCH SIMPLE
);
CREATE UNIQUE INDEX uk_clock_in_out_times_employee_id_date ON public.clock_in_out_times USING btree (employee_id, date);

