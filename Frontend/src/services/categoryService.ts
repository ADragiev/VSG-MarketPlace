import { ICategory } from "types";
import { baseApi } from "../utils/baseApi";

const GetCategories = "getCategories";

const categoryServices = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    [GetCategories]: builder.query<ICategory[],void>({
      query: () => "/Category",
    }),
  }),
});

export const { useGetCategoriesQuery } = categoryServices;
