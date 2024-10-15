import { baseApi } from '.';
import { IncomeSource, PagedResponse } from '../types';
import { getAllParams, transformErrorResponse } from './baseQueryWithReauth';

const api = baseApi.injectEndpoints({
  endpoints: (build) => ({
    getIncomeSources: build.query<PagedResponse<IncomeSource>, null>({
      query: () => ({
        url: '/income-sources',
        method: 'GET',
        params: {
          ...getAllParams,
        },
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Income-Sources'],
    }),

    addIncomeSource: build.mutation<object, object>({
      query: (body: object) => ({
        url: 'income-sources',
        method: 'POST',
        body: body,
      }),
      transformErrorResponse: transformErrorResponse,
      invalidatesTags: ['Income-Sources'],
    }),

    updateIncomeSource: build.mutation<object, object>({
      query: (
        body: object & {
          incomeSourceId: number;
        },
      ) => ({
        url: `income-sources/${body.incomeSourceId}`,
        method: 'PUT',
        body: body,
      }),
      transformErrorResponse: transformErrorResponse,
      invalidatesTags: ['Income-Sources'],
    }),
  }),
});

export const { useGetIncomeSourcesQuery, useAddIncomeSourceMutation, useUpdateIncomeSourceMutation } = api;
