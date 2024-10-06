INSERT INTO public.users VALUES 
(100, 'super-admin@example.com',   'SUPER-ADMIN@EXAMPLE.COM',      'super-admin@example.com',      'SUPER-ADMIN@EXAMPLE.COM',      true, 'AQAAAAIAAYagAAAAEIDoeTRLrGQvn8FRF2EXGdQ5GtreAkKzZcbl1BTD6xNNzUXrGkn6BCPp05RxjIH2lQ==', '2XTX6Z5RVMLHRXNWIMW3UJ2USO524VVU', '283d4f58-3a75-47ad-bf9c-de04c9e9a80a', '9876543210', false, false, NULL, true, 0, 0, '2024-05-03 03:51:44.770234-07', NULL, NULL),
(200, 'company-admin@example.com', 'COMPANY-ADMIN@EXAMPLE.COM',    'company-admin@example.com',    'COMPANY-ADMIN@EXAMPLE.COM',    true, 'AQAAAAIAAYagAAAAEIDoeTRLrGQvn8FRF2EXGdQ5GtreAkKzZcbl1BTD6xNNzUXrGkn6BCPp05RxjIH2lQ==', '2XTX6Z5RVMLHRXNWIMW3UJ2USO524VVU', '283d4f58-3a75-47ad-bf9c-de04c9e9a80a', '9876543211', false, false, NULL, true, 0, 0, '2024-05-03 03:51:44.770234-07', NULL, NULL),
(300, 'employee@example.com',      'EMPLOYEE@EXAMPLE.COM',         'employee@example.com',         'EMPLOYEE@EXAMPLE.COM',         true, 'AQAAAAIAAYagAAAAEIDoeTRLrGQvn8FRF2EXGdQ5GtreAkKzZcbl1BTD6xNNzUXrGkn6BCPp05RxjIH2lQ==', '2XTX6Z5RVMLHRXNWIMW3UJ2USO524VVU', '283d4f58-3a75-47ad-bf9c-de04c9e9a80a', '9876543212', false, false, NULL, true, 0, 0, '2024-05-03 03:51:44.770234-07', NULL, NULL);


INSERT INTO public.companies (id, owner_user_id, name, email, phone_number, financial_month, administrator_name, weekly_off_days, address, is_deleted, created_by, created_on)
VALUES
(999, 200, 'Test Company', 'company1@example.com', '9876543210', 3, 'Super Admin', ARRAY['Sunday'], 'Test Address', false, -1, '2024-05-03 03:31:32.227954-07');

INSERT INTO public.roles VALUES (1, 'SuperAdmin',   'SUPERADMIN',   NULL, 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);
INSERT INTO public.roles VALUES (2, 'CompanyAdmin', 'COMPANYADMIN', NULL, 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);
INSERT INTO public.roles VALUES (3, 'Employee',     'EMPLOYEE',     NULL, 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);

INSERT INTO public.user_roles VALUES (100, 1, 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);
INSERT INTO public.user_roles VALUES (200, 2, 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);
INSERT INTO public.user_roles VALUES (300, 3, 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);


INSERT INTO public.departments
(id, company_id, "name", created_by, created_on, modified_by, modified_on)
VALUES(3000, 999, 'Test-Department', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);


INSERT INTO public.job_titles
(id, company_id, department_id, "name", created_by, created_on, modified_by, modified_on)
VALUES(4000, 999, 3000, 'Test-Department', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);



INSERT INTO public.employees
(id, company_id, user_id, department_id, job_title_id, first_name, last_name, date_of_birth, date_of_joining, father_name, phone_number, company_email, personal_email, address, city, pan, aadhar, created_by, created_on, modified_by, modified_on)
VALUES
(1, 999, 300, 3000, 4000, 'Test-First-1', 'Test-Last-1', '2000-05-03', '2024-05-03', 'Test-Father-1', '01234567890', 'test1@example.com', 'test@example.com', 'Test-Address', 'Test-City', 'XXXXX0000X', '000000000000', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL),
(2, 999, 200, 3000, 4000, 'Test-First-2', 'Test-Last-2', '2000-05-03', '2024-05-03', 'Test-Father-2', '01234567890', 'test2@example.com', 'test@example.com', 'Test-Address', 'Test-City', 'XXXXX1111X', '111111111111', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);


SELECT setval('employees_id_seq', 3, false);