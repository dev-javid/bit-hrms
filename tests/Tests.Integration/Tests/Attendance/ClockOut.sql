INSERT INTO public.clock_in_out_times
(id, employee_id, date, clock_in_time, clock_out_time, created_by, created_on, modified_by, modified_on, company_id)
VALUES
(3, 1, NOW()::DATE, (current_timestamp AT TIME ZONE 'UTC') - interval '10 hours', NULL, 0, '2024-05-03 03:31:32.227954-07', NULL, NULL, 999);
