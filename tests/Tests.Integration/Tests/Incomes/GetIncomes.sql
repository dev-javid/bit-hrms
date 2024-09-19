INSERT INTO income_sources (id, company_id, name, description, created_on, created_by, modified_by, modified_on)
VALUES (1,  999, 'Income Source 1', 'Description 1',  '2024-05-03 03:51:44.770234-07', 100, null, null),
       (2,  999, 'Income Source 2', 'Description 2',  '2024-06-03 03:51:44.770234-07', 100, null, null);

INSERT INTO incomes (id, company_id, income_source_id, amount, documents, remarks, created_on, created_by, modified_by, modified_on)
VALUES (1, 999, 1, decode('r9a0jnes21bjwubV9WWhyD471a+GfkfaSxfWvm/EipI=', 'base64'), ARRAY['test.png', 'test2.png'], null, '2024-05-03 03:51:44.770234-07', 100, null, null),
       (2, 999, 2, decode('NCreznXjp8Mj+MhlL7E1VJCTGtJ6yHRnlMW3SslYM74=', 'base64'), ARRAY['test.png', 'test2.png'], null, '2024-05-04 03:51:44.770234-07', 100, null, null);

