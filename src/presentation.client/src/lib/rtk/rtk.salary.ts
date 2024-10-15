import { baseApi } from '.';
import { Compensation, EstimatedSalary, PagedResponse, Salary } from '../types';
import { getAllParams, transformErrorResponse } from './baseQueryWithReauth';

const api = baseApi.injectEndpoints({
  endpoints: (build) => ({
    getCompensation: build.query<Compensation[], { employeeId: number | undefined }>({
      query: (params) => ({
        url: '/compensations',
        method: 'GET',
        params: params.employeeId ? { employeeId: params.employeeId } : {},
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Compensation'],
    }),

    addCompensation: build.mutation<object, object>({
      query: (
        body: object & {
          effectiveFrom: string;
        },
      ) => ({
        url: 'compensations',
        method: 'POST',
        body: {
          ...body,
          effectiveFrom: body.effectiveFrom,
        },
      }),
      invalidatesTags: ['Compensation'],
    }),

    getSalaries: build.query<PagedResponse<Salary>, { month: number; year: number }>({
      query: (data: { month: number; year: number }) => ({
        url: '/salaries',
        method: 'GET',
        params: {
          ...getAllParams,
          month: data.month,
          year: data.year,
        },
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Salaries'],
    }),

    getEstimatedSalary: build.query<EstimatedSalary, { employeeId: number; month: number; year: number }>({
      query: (data: { employeeId: number; month: number; year: number }) => ({
        url: '/salaries/estimated-salary',
        method: 'GET',
        params: {
          month: data.month,
          year: data.year,
          employeeId: data.employeeId,
        },
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Salaries'],
    }),

    addSalary: build.mutation<object, object>({
      query: (body) => ({
        url: 'salaries',
        method: 'POST',
        body: body,
      }),
      invalidatesTags: ['Salaries'],
    }),
  }),
});

export const { useGetCompensationQuery, useAddCompensationMutation, useGetSalariesQuery, useGetEstimatedSalaryQuery, useAddSalaryMutation } = api;
