import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

export const baseURL =
  "https://auto.loanvantage360.com/internship/EvaluationSystemAngel";

  
  export const baseApi = createApi({
    reducerPath: "baseApi",
    baseQuery: fetchBaseQuery({
      baseUrl: baseURL,
      prepareHeaders: (headers) => {
      const user = JSON.parse(sessionStorage.getItem("user") as string);
      const token = user.token;
      headers.set("Authorization", `Bearer ${token}`);
      return headers;
    },
  }),
  endpoints: () => ({}),
  keepUnusedDataFor: 0,
  refetchOnMountOrArgChange: true,
});
