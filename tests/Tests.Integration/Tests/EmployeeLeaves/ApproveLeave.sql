INSERT INTO public.employee_leaves (id, company_id, employee_id, from_date, to_date, status, remarks, created_by, created_on, modified_by, modified_on)
VALUES
(1, 999, 1, '2024-05-03', '2024-05-03', 'Pending', 'remarks', 0,'2024-05-03 03:31:32.227954-07', NULL, NULL);


INSERT INTO public.leave_policies (id, company_id, casual_leaves, earned_leaves_per_month, holidays, created_by, created_on,  modified_by, modified_on)
VALUES
(1, 999, 1, 1, 1, 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);
