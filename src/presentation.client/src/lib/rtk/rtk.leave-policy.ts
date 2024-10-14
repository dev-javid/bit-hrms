import { baseApi } from '.';
import { LeavePolicy } from '../types';
import { transformErrorResponse } from './baseQueryWithReauth';

const api = baseApi.injectEndpoints({
  endpoints: (build) => ({
    getLeavePolicy: build.query<LeavePolicy, null>({
      query: () => ({
        url: '/leave-policy',
        method: 'GET',
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Leave-Policy'],
    }),
    updateLeavePolicy: build.mutation<LeavePolicy, object>({
      query: (body: object) => ({
        url: `leave-policy`,
        method: 'PUT',
        body: body,
      }),
      invalidatesTags: ['Leave-Policy', 'Holidays'],
    }),
  }),
});

export const { useGetLeavePolicyQuery, useUpdateLeavePolicyMutation } = api;
