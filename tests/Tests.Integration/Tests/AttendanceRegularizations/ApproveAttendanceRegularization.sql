INSERT INTO public.clock_in_out_times
(id, employee_id, date, clock_in_time, clock_out_time, created_by, created_on, modified_by, modified_on, company_id)
VALUES
(1, 1, '2024-07-05', '08:00:00', NULL, 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999);

INSERT INTO public.attendance_regularization
(id, company_id, employee_id, date, clock_in_time, clock_out_time, reason, approved, created_by, created_on, modified_by, modified_on)
VALUES
(3, 999, 1, '2024-07-05', '08:00:00', '16:00:00', 'Feeling not well', false, 0, '2024-05-03 03:31:32.227954-07', NULL, NULL),
(4, 999, 2, '2024-06-05', '08:00:00', '17:00:00', 'Feeling not well', false, 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);


