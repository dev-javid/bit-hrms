import { baseApi } from '.';
import { EmployeeLeave, PagedResponse } from '../types';
import { getAllParams, transformErrorResponse } from './baseQueryWithReauth';

const api = baseApi.injectEndpoints({
  endpoints: (build) => ({
    getEmployeeLeaves: build.query<PagedResponse<EmployeeLeave>, object>({
      query: (data) => ({
        url: '/employee-leaves',
        method: 'GET',
        params: {
          ...data,
          ...getAllParams,
        },
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Employee-Leaves'],
    }),
    addEmployeeLeave: build.mutation<EmployeeLeave, object>({
      query: (
        data: object & {
          from: string;
          to: string;
        },
      ) => ({
        url: 'employee-leaves',
        method: 'POST',
        body: {
          ...data,
          from: data.from,
          to: data.to,
        },
      }),
      invalidatesTags: ['Employee-Leaves', 'Reports'],
    }),
    deleteEmployeeLeave: build.mutation<boolean, object>({
      query: (
        data: object & {
          employeeLeaveId: number;
        },
      ) => ({
        url: `employee-leaves/${data.employeeLeaveId}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Employee-Leaves', 'Reports'],
    }),
    approveEamployeeLeave: build.mutation<boolean, object>({
      query: (
        data: object & {
          employeeLeaveId: number;
        },
      ) => ({
        url: `employee-leaves/${data.employeeLeaveId}/approve`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: ['Employee-Leaves', 'Reports'],
    }),
    declineEamployeeLeave: build.mutation<boolean, object>({
      query: (
        data: object & {
          employeeLeaveId: number;
          remarks: string;
        },
      ) => ({
        url: `employee-leaves/${data.employeeLeaveId}/decline`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: ['Employee-Leaves', 'Reports'],
    }),
  }),
});

export const {
  useGetEmployeeLeavesQuery,
  useAddEmployeeLeaveMutation,
  useDeleteEmployeeLeaveMutation,
  useApproveEamployeeLeaveMutation,
  useDeclineEamployeeLeaveMutation,
} = api;
