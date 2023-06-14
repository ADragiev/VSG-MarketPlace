import { baseApi } from "../utils/baseApi";

const GetLentItems = "getLentItems";
const GetMyLentItems = 'getMyLentItems'
const PostLentItem = "postLentItem";
const ReturnLentItem = "returnLentItem";

const lentItemsService = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    [GetLentItems]: builder.query<any,void>({
        query: () => "/LentItem",
      }),
      [GetMyLentItems]: builder.query({
        query: () => "/LentItem/My-Lent-Items",
      }),
      [PostLentItem]: builder.mutation({
        query: (data) => ({
          method: "POST",
          url: `/LentItem`,
          body: data,
        }),
      }),
      [ReturnLentItem]: builder.mutation({
        query: (id) => ({
          method: "PUT",
          url: `/LentItem/${id}`,
        }),
      }),
  }),
});

export const { useGetLentItemsQuery,useGetMyLentItemsQuery, usePostLentItemMutation, useReturnLentItemMutation } = lentItemsService;