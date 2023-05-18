import { baseApi } from "../utils/baseApi";

const GetPendingOrders = "getPendingOrders";
const GetMyOrders = "getMyOrders";
const ConfirmOrder = "confirmOrder";
const RejectOrder = "rejectOrder";
const CreateOrder = "createOrder";

const ordersServices = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    [GetPendingOrders]: builder.query({
      query: () => "/Order",
    }),
    [GetMyOrders]: builder.query({
      query: () => "/MyOrders",
    }),
    [ConfirmOrder]: builder.mutation({
      query: (id) => ({
        method: "PUT",
        url: `/Order/${id}`,
      }),
    }),
    [RejectOrder]: builder.mutation({
      query: (id) => ({
        method: "DELETE",
        url: `/Order/${id}`,
      }),
    }),
    [CreateOrder]: builder.mutation({
      query: (data) => ({
        method: "POST",
        url: `/Order`,
        body: data,
      }),
    }),
  }),
});

export const {
  useGetPendingOrdersQuery,
  useGetMyOrdersQuery,
  useConfirmOrderMutation,
  useRejectOrderMutation,
  useCreateOrderMutation,
} = ordersServices;
