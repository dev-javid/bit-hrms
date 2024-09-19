INSERT INTO public.departments (id, company_id, name, created_by, created_on)
VALUES
(1, 999, 'HR',          1, now()),
(2, 999, 'Finance',     1, now()),
(3, 999, 'Engineering', 1, now()),
(4, 998, 'Marketing',   1, now()),
(5, 998, 'Sales',       1, now()),
(6, 998, 'Support',     1, now()),
(7, 998, 'IT',          1, now()),
(8, 998, 'Legal',       1, now()),
(9, 998, 'Operations',  1, now());


INSERT INTO public.job_titles
(id, company_id, department_id, "name", created_by, created_on, modified_by, modified_on)
VALUES
(4001, 999, 3000, 'Test-Department3', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL),
(4002, 999, 3000, 'Test-Department4', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL),
(4003, 999, 3000, 'Test-Department5', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL),
(4004, 999, 3000, 'Test-Department6', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);



INSERT INTO public.employees
(id, company_id, user_id, department_id, job_title_id, first_name, last_name, date_of_birth, date_of_joining, father_name, phone_number, company_email, personal_email, address, city, pan, aadhar, created_by, created_on, modified_by, modified_on)
VALUES
(3, 999, 300, 3000, 4000, 'Test-First-3', 'Test-Last-3', '2000-05-03', '2024-05-03', 'Test-Father-3', '01234567891', 'test3@example.com', 'test6@example.com', 'Test-Address', 'Test-City', 'XXXXX0000X', '000000000000', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL),
(4, 999, 200, 3000, 4000, 'Test-First-4', 'Test-Last-4', '2000-05-03', '2024-05-03', 'Test-Father-4', '01234567891', 'test4@example.com', 'test8@example.com', 'Test-Address', 'Test-City', 'XXXXX1111X', '111111111111', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);

INSERT INTO public.leave_policies (id, company_id, casual_leaves, earned_leaves_per_month, holidays, created_by, created_on)
VALUES
(1, 999, 3, 1, 2, 1, now()),
(2, 998, 5, 1, 6, 1, now());
