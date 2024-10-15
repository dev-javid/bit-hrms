import { fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { AuthTokens, deleteAauthTokens, getAuthTokens, getDeviceId } from '../types';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const BASE_URL = ((import.meta as any).env.VITE_API_URL ?? '') + '/api';

import type { BaseQueryFn, FetchArgs, FetchBaseQueryError } from '@reduxjs/toolkit/query';
import { setRefreshingToken, setUser } from '../store/slice';

const baseQuery = fetchBaseQuery({
  baseUrl: BASE_URL,
  prepareHeaders: (headers) => {
    const deviceId = getDeviceId();
    headers.set('X-DeviceId', deviceId);

    const token = getAuthTokens()?.accessToken;
    if (token) {
      headers.set('Authorization', `Bearer ${token}`);
    }
    return headers;
  },
});

const baseQueryWithReauth: BaseQueryFn<string | FetchArgs, unknown, FetchBaseQueryError> = async (args, api, extraOptions) => {
  const user = api.getState() as {
    slice: {
      sessionActive: boolean;
    };
  };

  const token = getAuthTokens()?.accessToken;
  if (token && !user?.slice?.sessionActive) {
    await refreshingToken(api, extraOptions);
  }

  let result = await baseQuery(removeNullQueryParams(args), api, extraOptions);
  if (result.error && result.error.status === 401) {
    if (await refreshingToken(api, extraOptions)) {
      result = await baseQuery(args, api, extraOptions);
    }
  }
  return result;
};

export function transformErrorResponse(
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  baseQueryReturnValue: any,
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  meta: any,
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  arg: any,
) {
  console.log(meta);
  console.log(arg);
  return baseQueryReturnValue.data;
}

const logout = () => {
  deleteAauthTokens();
  window.location.href = '/';
};

const getAllParams = {
  page: 1,
  limit: 1000,
};

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export const removeNullQueryParams = (args: any): string | FetchArgs => {
  if (typeof args === 'string') {
    return args;
  } else if (args && typeof args === 'object') {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const cleanArgs: any = {};

    for (const key in args) {
      if (args[key] !== null && args[key] !== undefined) {
        cleanArgs[key] = args[key];
      }
    }
  }

  return args as FetchArgs;
};

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const refreshingToken = async (api: any, extraOptions: any) => {
  const tokens = getAuthTokens();
  if (tokens) {
    api.dispatch(setRefreshingToken(true));
    const refreshResult = await baseQuery(
      {
        url: '/auth/refresh-token',
        body: tokens,
        method: 'POST',
      },
      api,
      extraOptions,
    );
    api.dispatch(setRefreshingToken(false));
    if (refreshResult.data) {
      api.dispatch(setUser(refreshResult.data as AuthTokens));
      return true;
    } else {
      logout();
    }
  } else {
    logout();
  }
  return false;
};

export default baseQueryWithReauth;
export { getAllParams };
