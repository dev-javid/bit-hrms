import { createApi } from '@reduxjs/toolkit/query/react';
import baseQueryWithReauth from './baseQueryWithReauth';

export const baseApi = createApi({
  reducerPath: 'baseApi',

  baseQuery: baseQueryWithReauth,

  tagTypes: [
    'Auth',
    'Departments',
    'Holidays',
    'Companies',
    'Leave-Policy',
    'Employees',
    'JobTitles',
    'Employee-Leaves',
    'Reports',
    'Attendance',
    'Compensation',
    'Salaries',
    'Income-Sources',
  ],

  endpoints: () => ({}),
});
