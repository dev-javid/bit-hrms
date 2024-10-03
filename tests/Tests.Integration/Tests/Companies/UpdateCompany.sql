INSERT INTO public.companies (id, name, email, phone_number, financial_month, administrator_name, weekly_off_days, address, is_deleted, created_by, created_on)
VALUES 
(1,  'Test Company 1',  'test1@company.com', '1234567890', 1, 'Admin 1', ARRAY['Sunday'], 'Test Address 1', false, 1, now());
