import { baseApi } from '.';
import { Holiday, PagedResponse } from '../types';
import { getAllParams, transformErrorResponse } from './baseQueryWithReauth';

const api = baseApi.injectEndpoints({
  endpoints: (build) => ({
    getHolidays: build.query<PagedResponse<Holiday>, null>({
      query: () => ({
        url: '/holidays',
        method: 'GET',
        params: getAllParams,
      }),
      transformErrorResponse: transformErrorResponse,
      providesTags: ['Holidays'],
    }),
    addHoliday: build.mutation<Holiday, object>({
      query: (
        body: object & {
          date: string;
        },
      ) => ({
        url: 'holidays',
        method: 'POST',
        body: {
          ...body,
          date: body.date,
        },
      }),
      invalidatesTags: ['Holidays'],
    }),
    updateHoliday: build.mutation<Holiday, object>({
      query: (
        body: object & {
          holidayId: number;
          date: string;
        },
      ) => ({
        url: `holidays/${body.holidayId}`,
        method: 'PUT',
        body: {
          ...body,
          date: body.date,
        },
      }),
      invalidatesTags: ['Holidays'],
    }),
  }),
});

export const { useGetHolidaysQuery, useAddHolidayMutation, useUpdateHolidayMutation } = api;
