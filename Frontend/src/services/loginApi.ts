import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import LoginRequest from '../models/LoginRequest';
import LoginResponse from '../models/LoginResponse';

export const loginApi = createApi({
  reducerPath: 'loginApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'http://localhost:5237/api/Login',
    prepareHeaders: (headers, { getState }) => {
      headers.set('Content-Type', 'application/json')
      return headers
    }

  }),
  endpoints: builder => ({
    login: builder.mutation<LoginResponse, LoginRequest>({
      query: (req) => ({
        url: "Login",
        method: "POST",
        body: JSON.stringify(req),
      }),
    }),
  }),
})
export const { useLoginMutation } = loginApi;
