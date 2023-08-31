import { IMyOrder, IPendingOrder } from "types";
import { baseApi } from "../utils/baseApi";

const GetPendingOrders = "getPendingOrders";
const GetMyOrders = "getMyOrders";
const ConfirmOrder = "confirmOrder";
const RejectOrder = "rejectOrder";
const CreateOrder = "createOrder";

const ordersServices = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    [GetPendingOrders]: builder.query<IPendingOrder[],void>({
      query: () => "/Order",
    }),
    [GetMyOrders]: builder.query<IMyOrder[],void>({
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
