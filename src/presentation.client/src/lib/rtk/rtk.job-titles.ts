import { baseApi } from '.';
import { JobTitle, PagedResponse } from '../types';
import { getAllParams, transformErrorResponse } from './baseQueryWithReauth';

const api = baseApi.injectEndpoints({
  endpoints: (build) => ({
    getJobTitles: build.query<PagedResponse<JobTitle>, { departmentId?: number }>({
      query: ({ departmentId }) => ({
        url: `/job-titles`,
        method: 'GET',
        params: departmentId
          ? {
              ...getAllParams,
              departmentId,
            }
          : getAllParams,
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['JobTitles'],
    }),
    addJobTitle: build.mutation<JobTitle, object>({
      query: (body: object) => ({
        url: 'job-titles',
        method: 'POST',
        body: body,
      }),
      invalidatesTags: ['JobTitles'],
    }),
    updateJobTitle: build.mutation<JobTitle, object>({
      query: (body: object & { jobTitleId: number }) => ({
        url: `job-titles/${body.jobTitleId}`,
        method: 'PUT',
        body: body,
      }),
      invalidatesTags: ['JobTitles'],
    }),
  }),
});

export const { useGetJobTitlesQuery, useAddJobTitleMutation, useUpdateJobTitleMutation } = api;
