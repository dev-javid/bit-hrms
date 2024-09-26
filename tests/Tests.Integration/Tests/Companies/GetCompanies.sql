INSERT INTO public.companies (id, name, email, phone_number, financial_month, administrator_name, weekly_off_days, is_deleted, created_by, created_on)
VALUES 
(1,  'Test Company 1',  'test1@company.com', '1234567890', 1, 'Admin 1', ARRAY['Sunday'], false, 1, now()),
(2,  'Test Company 2',  'test2@company.com', '2345678901', 2, 'Admin 2', ARRAY['Sunday'], false, 1, now()),
(3,  'Test Company 3',  'test3@company.com', '3456789012', 3, 'Admin 3', ARRAY['Sunday'], false, 1, now()),
(4,  'Test Company 4',  'test4@company.com', '4567890123', 4, 'Admin 4', ARRAY['Sunday'], false, 1, now()),
(5,  'Test Company 5',  'test5@company.com', '5678901234', 5, 'Admin 5', ARRAY['Sunday'], false, 1, now()),
(6,  'Test Company 6',  'test6@company.com', '6789012345', 6, 'Admin 6', ARRAY['Sunday'], false, 1, now()),
(7,  'Test Company 7',  'test7@company.com', '7890123456', 7, 'Admin 7', ARRAY['Sunday'], false, 1, now()),
(8,  'Test Company 8',  'test8@company.com', '8901234567', 8, 'Admin 8', ARRAY['Sunday'], false, 1, now()),
(9,  'Test Company 9',  'test9@company.com', '9012345678', 9, 'Admin 9', ARRAY['Sunday'], false, 1, now()),
(10, 'Test Company 10', 'test9@company.com', '9012345678', 9, 'Admin 9', ARRAY['Sunday'], true,  1, now());
