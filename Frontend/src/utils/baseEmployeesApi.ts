import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { IEmployee, IEmployeesResponse } from "types";

export const baseURL = "https://sm-server.netlify.app/.netlify/functions/";

export const baseEmployeesApi = createApi({
  reducerPath: "baseEmployeesApi",
  baseQuery: fetchBaseQuery({
    baseUrl: baseURL,
    prepareHeaders: (headers) => {
      headers.set("x-token", `vanessa&radostina`);
      return headers;
    },
  }),
  endpoints: (builder) => ({
    getEmployees: builder.query<IEmployee[],void>({
      query: () => "/get_all_employees_data_from_bob", 
      transformResponse: (response: IEmployeesResponse)=>{
        return response.employees
      }
    }),
  }),
  keepUnusedDataFor: 86400,
});

export const { useGetEmployeesQuery } = baseEmployeesApi;
