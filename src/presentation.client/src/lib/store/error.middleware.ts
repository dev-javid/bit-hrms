import { Middleware, isRejected } from '@reduxjs/toolkit';
import { setError } from './slice';

export const rtkQueryErrorLogger: Middleware = (store) => (next) => (action) => {
  if (isRejected(action)) {
    let error = 'An error occured.';
    if (typeof action.payload === 'string') {
      error = action.payload;
    } else if (action.payload) {
      const { status, data } = action.payload as {
        status: number;
        data: string;
      };
      error = getErrorMessage(status, data);
    } else if (action.meta) {
      const { status, data } =
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        (action.meta as any)?.baseQueryMeta?.response ?? {};
      error = getErrorMessage(status, data);
    }
    store.dispatch(setError(error));
  }

  return next(action);
};

const getErrorMessage = (status: number, data: string) => {
  let error = 'An error occured.';
  if (status !== 401) {
    if (status == 404) {
      error = 'The resource you are looking for could not be found.';
    }
    if (status == 403) {
      error = 'You are not authorized to perform this action.';
    } else if (status?.toString() == 'FETCH_ERROR') {
      error = 'We are unable to connect to the backend server. Please contact administrator.';
    } else if (data) {
      error = data;
    } else {
      error = 'We are unable to connect to the backend server. Please contact administrator.';
    }
  }

  return error;
};
