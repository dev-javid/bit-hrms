import { baseApi } from '.';
import { Employee, PagedResponse } from '../types';
import { getAllParams, transformErrorResponse } from './baseQueryWithReauth';

const api = baseApi.injectEndpoints({
  endpoints: (build) => ({
    getEmployee: build.query<Employee, { employeeId: number }>({
      query: (data: { employeeId: number }) => ({
        url: `/employees/${data.employeeId}`,
        method: 'GET',
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Employees'],
    }),
    getEmployees: build.query<PagedResponse<Employee>, null>({
      query: () => ({
        url: '/employees',
        method: 'GET',
        params: getAllParams,
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Employees'],
    }),
    addEmployee: build.mutation<Employee, object>({
      query: (
        data: object & {
          dateOfBirth: string;
          dateOfJoining: string;
        },
      ) => ({
        url: 'employees',
        method: 'POST',
        body: {
          ...data,
          dateOfBirth: data.dateOfBirth,
          dateOfJoining: data.dateOfJoining,
        },
      }),
      invalidatesTags: ['Employees'],
    }),
    updateEmployee: build.mutation<Employee, object>({
      query: (
        data: object & {
          employeeId: number;
          dateOfBirth: string;
          dateOfJoining: string;
        },
      ) => ({
        url: `employees/${data.employeeId}`,
        method: 'PUT',
        body: {
          ...data,
          dateOfBirth: data.dateOfBirth,
          dateOfJoining: data.dateOfJoining,
        },
      }),
      invalidatesTags: ['Employees'],
    }),
    setEmployeeDocument: build.mutation<boolean, object>({
      query: (
        data: object & {
          employeeId: number;
        },
      ) => ({
        url: `employees/${data.employeeId}/documents`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: ['Employees'],
    }),
  }),
});

export const { useGetEmployeeQuery, useGetEmployeesQuery, useAddEmployeeMutation, useUpdateEmployeeMutation, useSetEmployeeDocumentMutation } = api;
