import { baseApi } from '.';
import { BasicAdminReport, BasicEmployeeReport } from '../types';
import { transformErrorResponse } from './baseQueryWithReauth';

const api = baseApi.injectEndpoints({
  endpoints: (build) => ({
    getBasicAdminReport: build.query<BasicAdminReport, null>({
      query: () => ({
        url: '/reports/admin-basic',
        method: 'GET',
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Reports'],
    }),
    getBasicEmployeeReport: build.query<
      BasicEmployeeReport,
      {
        employeeId?: number;
      }
    >({
      query: (p) => ({
        url: '/reports/employee-basic',
        method: 'GET',
        params: p,
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Reports'],
    }),
  }),
});

export const { useGetBasicAdminReportQuery, useGetBasicEmployeeReportQuery } = api;
