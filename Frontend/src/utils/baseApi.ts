import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

export const baseURL =
  "https://auto.loanvantage360.com/internship/EvaluationSystemAngel";

  
  export const baseApi = createApi({
    reducerPath: "baseApi",
    baseQuery: fetchBaseQuery({
      baseUrl: baseURL,
      prepareHeaders: (headers) => {
      const user = JSON.parse(sessionStorage.getItem("user") as string);
      headers.set("Authorization", `Bearer ${user?.token}`);
      return headers;
    },
  }),
  endpoints: () => ({}),
  keepUnusedDataFor: 0,
  refetchOnMountOrArgChange: true,
});
