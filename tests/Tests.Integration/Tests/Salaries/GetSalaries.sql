INSERT INTO salaries (company_id, employee_id, month, year, amount, created_by, created_on) 
VALUES 
(999, 1, EXTRACT(MONTH FROM CURRENT_DATE), EXTRACT(YEAR FROM CURRENT_DATE),  decode('r9a0jnes21bjwubV9WWhyD471a+GfkfaSxfWvm/EipI=', 'base64'), 1, NOW()),
(999, 2, EXTRACT(MONTH FROM CURRENT_DATE), EXTRACT(YEAR FROM CURRENT_DATE),  decode('NCreznXjp8Mj+MhlL7E1VJCTGtJ6yHRnlMW3SslYM74=', 'base64'), 1, NOW()),
(999, 1, 6,                                2024,                             decode('OANFUK+Ot3vm9O7Aav81agrBBURnBQovD5N29lTHRWs=', 'base64'), 1, '2024-01-01 00:00:00+05:30'),
(999, 2, 6,                                2024,                             decode('l8MD4C6izZebR2+62C191mDNZbTeknGpQNfchAnu1Po=', 'base64'), 1, '2024-01-01 00:00:00+05:30');

