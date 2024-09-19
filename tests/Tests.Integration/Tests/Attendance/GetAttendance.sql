INSERT INTO public.clock_in_out_times
(id, employee_id, date, clock_in_time, clock_out_time, created_by, created_on, modified_by, modified_on, company_id)
VALUES
(3,  1, '2024-05-03', '08:00:00', '14:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(4,  1, '2024-06-04', '08:00:00', '15:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(5,  1, '2024-06-05', '08:00:00', '16:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(7,  2, '2024-07-07', '08:00:00', '14:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(8,  2, '2024-07-08', '08:00:00',  NULL,      0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(9,  1, '2024-06-06', '08:00:00',  NULL,      0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(10, 1, '2024-06-07', '09:00:00', '18:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(11, 1, '2024-06-08', '10:00:00', '19:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(12, 1, '2024-06-09', '11:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999);



INSERT INTO public.clock_in_out_times
(id, employee_id, date, clock_in_time, clock_out_time, created_by, created_on, modified_by, modified_on, company_id)
VALUES
(40,  1, DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '1 days', '08:00:00', '14:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(41,  1, DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '2 days', '08:00:00', '15:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(42,  1, DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '3 days', '08:00:00', '15:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(43,  1, DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '4 days', '08:00:00',  NULL,      0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(44,  1, DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '5 days', '08:00:00',  NULL,      0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(45,  1, DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '6 days', '08:00:00', '15:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(46,  1, DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '7 days', '08:00:00', '15:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(47,  1, DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '8 days', '08:00:00', '15:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999);


INSERT INTO attendance_regularization
(id, company_id, employee_id, date, clock_in_time, clock_out_time, reason, approved, created_by, created_on, modified_by, modified_on)
VALUES
(51, 999, 1, DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '7 days',  '08:00:00', '14:00:00', 'Doctor appointment',  true,  0, CURRENT_TIMESTAMP, NULL, NULL),
(48, 999, 1, '2024-07-08',                                           '09:00:00', '15:00:00', 'Family emergency',    false, 0, CURRENT_TIMESTAMP, NULL, NULL),
(52, 999, 1, '2024-06-06',                                           '09:00:00', '15:00:00', 'Family emergency',    false, 0, CURRENT_TIMESTAMP, NULL, NULL),
(53, 999, 1, '2024-06-26',                                           '09:00:00', '15:00:00', 'Not well',            true,  0, CURRENT_TIMESTAMP, NULL, NULL);
