import { IInventoryItem, IProduct } from "types";
import { baseApi } from "../utils/baseApi";

const GetProducts = "getProducts";
const GetInventoryProducts = "getInventoryProducts";
const UpdateProduct = "updateProduct";
const CreateProduct = "createProduct";
const DeleteProduct = "deleteProduct";

const productsServices = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    [GetProducts]: builder.query<IProduct[],void>({
      query: () => "/Product",
    }),
    [GetInventoryProducts]: builder.query<IInventoryItem[],void>({
      query: () => "/Product/Inventory",
    }),
    [CreateProduct]: builder.mutation({
      query: (data) => ({
        method: "POST",
        url: "/Product",
        body: data,
      }),
    }),
    [DeleteProduct]: builder.mutation({
      query: (id) => ({
        method: "DELETE",
        url: `/Product/${id}`,
      }),
    }),
    [UpdateProduct]: builder.mutation({
      query: ({ id, data }) => ({
        method: "PUT",
        url: `/Product/${id}`,
        body: data,
      }),
    }),
  }),
});

export const {
  useGetProductsQuery,
  useLazyGetProductsQuery,
  useCreateProductMutation,
  useDeleteProductMutation,
  useGetInventoryProductsQuery,
  useUpdateProductMutation,
} = productsServices;
