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
      query: () => "Order/My-Orders",
    }),
    [ConfirmOrder]: builder.mutation({
      query: (id) => ({
        method: "PUT",
        url: `/Order/Complete/${id}`,
      }),
    }),
    [RejectOrder]: builder.mutation({
      query: (id) => ({
        method: "PUT",
        url: `/Order/Reject/${id}`,
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
