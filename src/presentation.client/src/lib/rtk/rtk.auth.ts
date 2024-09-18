import { baseApi } from '.';
import { AuthTokens, Credentials, User } from '../types';

export const authApi = baseApi.injectEndpoints({
  endpoints: (build) => ({
    signIn: build.mutation<AuthTokens, Credentials>({
      query: (credentials: Credentials) => ({
        url: 'auth/sign-in',
        method: 'POST',
        body: credentials,
      }),
    }),
    signOut: build.mutation<null, null>({
      query: () => ({
        url: 'auth/sign-out',
        method: 'POST',
      }),
    }),
    setPassword: build.mutation<User, object>({
      query: (body: object) => ({
        url: 'auth/set-password',
        method: 'POST',
        body: body,
      }),
    }),
    resetPassword: build.mutation<User, object>({
      query: (body: object) => ({
        url: 'auth/forgot-password',
        method: 'POST',
        body: body,
      }),
    }),
    updatePassword: build.mutation<boolean, object>({
      query: (body: object) => ({
        url: 'auth/update-password',
        method: 'PUT',
        body: body,
      }),
    }),
    forgotPassword: build.mutation<boolean, object>({
      query: (body: object) => ({
        url: 'auth/forgot-password',
        method: 'POST',
        body: body,
      }),
    }),
  }),
});

export const {
  useSignInMutation,
  useSignOutMutation,
  useSetPasswordMutation,
  useResetPasswordMutation,
  useUpdatePasswordMutation,
  useForgotPasswordMutation,
} = authApi;
