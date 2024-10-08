﻿ -- MAY 2024
 -- Saturday Off - 4th, 18th
 -- Holiday on 28th
 -- Leaves approved for 7th & 8th
 -- Leave denied for 31th
 -- clock out missing on 2nd
 -- clock in missing on 22

INSERT INTO public.clock_in_out_times
(employee_id, date, clock_in_time, clock_out_time, created_by, created_on, modified_by, modified_on, company_id)
VALUES
(1, '2024-05-01', '08:00:00', '18:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-02', '08:00:00', NULL      , 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-03', '08:00:00', '18:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-06', '08:00:00', '18:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-09', '08:00:00', '19:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-10', '08:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-11', '08:00:00', '18:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-13', '08:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-14', '08:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-15', '08:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-16', '08:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-17', '08:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-20', '08:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-21', '08:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-23', '08:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-24', '08:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-25', '08:00:00', '18:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-27', '08:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-29', '08:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999),
(1, '2024-05-30', '08:00:00', '20:00:00', 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999);


INSERT INTO public.employee_leaves (company_id, employee_id, from_date, to_date, status, remarks,  created_by, created_on, modified_by, modified_on)
VALUES
(999, 1, '2024-05-07', '2024-05-18', 'Approved', 'Test', 0, '2024-05-04 03:33:32.227954-07', NULL, NULL),
(999, 1, '2024-06-08', '2024-06-27', 'Approved', 'Test', 0, '2024-05-04 03:33:32.227954-07', NULL, NULL),
(999, 1, '2024-05-31', '2024-05-31', 'Declined', 'Test', 0, '2024-05-04 03:33:32.227954-07', NULL, NULL);



INSERT INTO public.holidays (id, company_id, name, date, optional, created_by, created_on)
VALUES
(1, 999, 'Test',   '2024-05-28', 'false', 1, now());


INSERT INTO public.compensations
(id, company_id, employee_id, effective_from, amount, created_by, created_on, modified_by, modified_on)
VALUES
(1, 999, 1, '2024-03-03', decode('r9a0jnes21bjwubV9WWhyD471a+GfkfaSxfWvm/EipI=', 'base64'), 0, '2024-05-03 03:31:32.227954-07', NULL, NULL);