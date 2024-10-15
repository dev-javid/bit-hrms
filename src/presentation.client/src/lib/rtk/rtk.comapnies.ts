import { baseApi } from '.';
import { Company, PagedResponse } from '../types';
import { getAllParams, transformErrorResponse } from './baseQueryWithReauth';

const api = baseApi.injectEndpoints({
  endpoints: (build) => ({
    getCompanies: build.query<PagedResponse<Company>, null>({
      query: () => ({
        url: '/companies',
        method: 'GET',
        params: getAllParams,
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Companies'],
    }),
    getCompany: build.query<Company, number>({
      query: (companyId) => ({
        url: `/companies/${companyId}`,
        method: 'GET',
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Companies'],
    }),
    addCompany: build.mutation<Company, object>({
      query: (body: object) => ({
        url: 'companies',
        method: 'POST',
        body: body,
      }),
      invalidatesTags: ['Companies'],
    }),
    updateCompany: build.mutation<Company, object>({
      query: (
        body: object & {
          companyId: number;
        },
      ) => ({
        url: `companies/${body.companyId}`,
        method: 'PUT',
        body: body,
      }),
      invalidatesTags: ['Companies'],
    }),
    deleteCompany: build.mutation<void, number>({
      query: (companyId: number) => ({
        url: `companies/${companyId}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Companies'],
    }),
  }),
});

export const { useGetCompaniesQuery, useGetCompanyQuery, useAddCompanyMutation, useUpdateCompanyMutation, useDeleteCompanyMutation } = api;
