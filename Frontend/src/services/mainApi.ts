import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { RootState } from '../store/store';

export const mainApi = createApi({
  reducerPath: 'mainApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'http://localhost:5237/api/MainApi',
    prepareHeaders: (headers, { getState }) => {
      const token = (getState() as RootState)?.slice?.loginUser?.token;
      console.log('token', token)
      if(token)
        headers.set('Authorization', `Bearer ${token}`);
      return headers
    }
  }),
  tagTypes: ['userContent', 'adminContent'],
  endpoints: builder => ({
    adminEndpoint: builder.query<string, void>({
      query: () => ({
        url: "AdminEndpoint",
        responseHandler: "text"
      }),
      providesTags: ["adminContent"]
    }),
    publicEndpoint: builder.query<string, void>({
      query: () => ({
        url: "PublicEndpoint",
        responseHandler: "text"
      }),
      providesTags: ["userContent"]
    }),
  }),
})
export const { useAdminEndpointQuery, usePublicEndpointQuery} = mainApi;
