INSERT INTO public.companies (id, owner_user_id, name, email, phone_number, financial_month, administrator_name, weekly_off_days, address, is_deleted, created_by, created_on)
VALUES 
(1,  200, 'Test Company 1',  'test1@company.com', '1234567890', 1, 'Admin 1', ARRAY['Sunday'], 'Test Address 1',  false, 1, now()),
(2,  200, 'Test Company 2',  'test2@company.com', '2345678901', 2, 'Admin 2', ARRAY['Sunday'], 'Test Address 2',  false, 1, now()),
(3,  200, 'Test Company 3',  'test3@company.com', '3456789012', 3, 'Admin 3', ARRAY['Sunday'], 'Test Address 3',  false, 1, now()),
(4,  200, 'Test Company 4',  'test4@company.com', '4567890123', 4, 'Admin 4', ARRAY['Sunday'], 'Test Address 4',  false, 1, now()),
(5,  200, 'Test Company 5',  'test5@company.com', '5678901234', 5, 'Admin 5', ARRAY['Sunday'], 'Test Address 5',  false, 1, now()),
(6,  200, 'Test Company 6',  'test6@company.com', '6789012345', 6, 'Admin 6', ARRAY['Sunday'], 'Test Address 6',  false, 1, now()),
(7,  200, 'Test Company 7',  'test7@company.com', '7890123456', 7, 'Admin 7', ARRAY['Sunday'], 'Test Address 7',  false, 1, now()),
(8,  200, 'Test Company 8',  'test8@company.com', '8901234567', 8, 'Admin 8', ARRAY['Sunday'], 'Test Address 8',  false, 1, now()),
(9,  200, 'Test Company 9',  'test9@company.com', '9012345678', 9, 'Admin 9', ARRAY['Sunday'], 'Test Address 9',  false, 1, now()),
(10, 200, 'Test Company 10', 'test9@company.com', '9012345678', 9, 'Admin 9', ARRAY['Sunday'], 'Test Address 10', true,  1, now());
