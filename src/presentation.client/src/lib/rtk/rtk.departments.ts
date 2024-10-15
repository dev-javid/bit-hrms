import { baseApi } from '.';
import { Department, PagedResponse } from '../types';
import { getAllParams, transformErrorResponse } from './baseQueryWithReauth';

const api = baseApi.injectEndpoints({
  endpoints: (build) => ({
    getDepartments: build.query<PagedResponse<Department>, null>({
      query: () => ({
        url: '/departments',
        method: 'GET',
        params: getAllParams,
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Departments'],
    }),
    addDepartment: build.mutation<Department, object>({
      query: (body: object) => ({
        url: 'departments',
        method: 'POST',
        body: body,
      }),
      invalidatesTags: ['Departments'],
    }),
    updateDepartment: build.mutation<Department, object>({
      query: (
        body: object & {
          departmentId: number;
        },
      ) => ({
        url: `departments/${body.departmentId}`,
        method: 'PUT',
        body: body,
      }),
      invalidatesTags: ['Departments'],
    }),
  }),
});

export const { useGetDepartmentsQuery, useAddDepartmentMutation, useUpdateDepartmentMutation } = api;
