
INSERT INTO public.employee_leaves (id, company_id, employee_id, from_date, to_date, status, remarks,  created_by, created_on, modified_by, modified_on)
VALUES
(1, 998, 1, '2024-05-03', '2024-05-05', 0, 'Approved', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL),
(2, 999, 1, '2024-01-03', '2024-01-05', 0, 'Approved', 0, '2024-05-04 03:33:32.227954-07', NULL, NULL),
(3, 999, 1, '2024-02-03', '2024-02-05', 0, 'Approved', 0, '2024-05-05 03:32:32.227954-07', NULL, NULL),
(4, 999, 1, '2024-03-03', '2024-03-05', 0, 'Approved', 0, '2024-05-03 03:37:32.227954-07', NULL, NULL),
(5, 999, 1, '2024-04-03', '2024-04-05', 0, 'Approved', 0, '2024-05-03 03:36:32.227954-07', NULL, NULL),
(6, 999, 1, '2024-05-03', '2024-05-05', 0, 'pending',  0, '2024-05-03 03:35:32.227954-07', NULL, NULL),
(7, 999, 1, '2024-06-03', '2024-06-05', 0, 'Pending',  0, '2024-05-03 03:34:32.227954-07', NULL, NULL);


INSERT INTO public.leave_policies(id, company_id, casual_leaves, earned_leaves_per_month, holidays, created_by, created_on,  modified_by, modified_on)
VALUES
(1, 998, 1, 2, 1, 0, '2024-05-03 03:31:32.227954-07', NULL, NULL),
(2, 999, 3, 4, 5, 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);