ALTER TABLE public.salaries ADD "year" int NOT NULL;
ALTER TABLE public.salaries ADD CONSTRAINT uk_salaries_employee_id_month_year UNIQUE (employee_id,"month","year");
