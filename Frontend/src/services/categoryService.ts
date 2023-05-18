import { baseApi } from "../utils/baseApi";

const GetCategories = "getCategories";

const categoryServices = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    [GetCategories]: builder.query({
      query: () => "/Category",
    }),
  }),
});

export const { useGetCategoriesQuery } = categoryServices;
