import { baseApi } from '.';
import { AttendanceRegularization, InOutTiming, PagedResponse } from '../types';
import { getAllParams, transformErrorResponse } from './baseQueryWithReauth';

const api = baseApi.injectEndpoints({
  endpoints: (build) => ({
    getAttendance: build.query<
      InOutTiming[],
      {
        employeeId?: number;
        date?: string;
        month?: number;
        year?: number;
      }
    >({
      query: (params) => ({
        url: '/attendance',
        method: 'GET',
        params: {
          employeeId: params.employeeId,
          month: params.month,
          year: params.year,
          date: params.date,
        },
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Attendance'],
    }),
    clockIn: build.mutation<null, null>({
      query: () => ({
        url: '/attendance/clock-in',
        method: 'PUT',
        body: {},
      }),
      transformErrorResponse: transformErrorResponse,
      invalidatesTags: ['Attendance'],
    }),
    clockOut: build.mutation<null, null>({
      query: () => ({
        url: '/attendance/clock-out',
        method: 'PUT',
        body: {},
      }),
      transformErrorResponse: transformErrorResponse,
      invalidatesTags: ['Attendance'],
    }),
    getRegularizations: build.query<PagedResponse<AttendanceRegularization>, { employeeId?: number }>({
      query: (params: { employeeId?: number }) => ({
        url: '/attendance-regularizations',
        method: 'GET',
        params: {
          ...getAllParams,
          employeeId: params.employeeId,
        },
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Attendance'],
    }),
    addRegularization: build.mutation<null, object>({
      query: (body: object) => ({
        url: '/attendance-regularizations',
        method: 'POST',
        body: body,
      }),
      transformErrorResponse: transformErrorResponse,
      invalidatesTags: ['Attendance'],
    }),
    approveRegularization: build.mutation<null, { attendanceRegularizationId: number }>({
      query: (data: { attendanceRegularizationId: number }) => ({
        url: `/attendance-regularizations/${data.attendanceRegularizationId}/approve`,
        method: 'PUT',
        body: {},
      }),
      transformErrorResponse: transformErrorResponse,
      invalidatesTags: ['Attendance'],
    }),
  }),
});

export const {
  useGetAttendanceQuery,
  useClockInMutation,
  useClockOutMutation,
  useAddRegularizationMutation,
  useGetRegularizationsQuery,
  useApproveRegularizationMutation,
} = api;
