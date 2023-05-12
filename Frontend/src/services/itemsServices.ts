import { ICategory, IInventoryItem, ILocation, IMyOrder, IOrder, IPendingOrder, IProduct } from "../types";
import { makeRequest } from "./makeRequest";


export const loadProducts = async () => {
    return makeRequest<IProduct[]>({ path: "/Product" });
};
export const loadInventoryItems = async () => {
    return  makeRequest<IInventoryItem[]>({ path: "/Product/Inventory" });
};
export const loadProductsDetails = async (id: number) => {
  return makeRequest({ path: "/Product/Details/" + id });
};

export const loadPendingOrders = async () => {
  return makeRequest<IPendingOrder[]>({ path: "/Order" });
};
export const loadMyOrders = async () => {
  return makeRequest<IMyOrder[]>({ path: "/MyOrders" });
};

export const deleteProduct = async (id: number) => {
  return makeRequest<IProduct>({
      path: "/Product/" + id,
      method: "DELETE",
    });
};
export const confirmOrder = async (id: number) => {
  return makeRequest<IPendingOrder>({
      path: "/Order/" + id,
      method: "PUT",
    });
};
export const rejectOrder = async (id: number) => {
  return makeRequest<IOrder>({
      path: "/Order/" + id,
      method: "DELETE",
    });
};

export const createOrder = async (productId: number, qty: number) => {
  const data = {
    qty,
    productId,
  };
  return makeRequest({
      path: "/Order",
      method: "POST",
      data
    });
};

export const loadCategories = async () => {
  return makeRequest<ICategory[]>({ path: "/Category" });
};
export const loadLocations = async () => {
  return makeRequest<ILocation[]>({ path: "/Location" });

};

export const loadProductById = async (id: number) => {
  return makeRequest({ path: "/Product/" + id });

};
