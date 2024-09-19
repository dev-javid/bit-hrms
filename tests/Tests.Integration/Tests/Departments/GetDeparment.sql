INSERT INTO public.departments (id, company_id, name, created_by, created_on)
VALUES
(1, 999, 'HR',          1, now()),
(2, 999, 'Finance',     1, now()),
(3, 999, 'Engineering', 1, now()),
(4, 998, 'Marketing',   1, now());


INSERT INTO public.job_titles (id, company_id,  department_id, name, created_by, created_on)
VALUES
(1, 999, 1, 'Teacher',  1, now())
