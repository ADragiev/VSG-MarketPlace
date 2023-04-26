import { makeRequest } from "./makeRequest.js";

export const loadProducts = async () => {
  try {
    return await makeRequest({ path: "/Product" });
  } catch (err) {
    console.error(err);
  }
};
export const loadInventoryItems = async () => {
  try {
    return await makeRequest({ path: "/Product/Inventory" });
  } catch (err) {
    console.error(err);
  }
};
export const loadProductsDetails = async (id) => {
  try {
    return await makeRequest({ path: "/Product/Details/" + id });
  } catch (err) {
    console.error(err);
  }
};

export const loadPendingOrders = async () => {
  try {
    return await makeRequest({ path: "/Order" });
  } catch (err) {
    console.error(err);
  }
};
export const postImageById = async (id, image) => {
  try {
    return await makeRequest({
      path: "/Image/" + id,
      method: "POST",
      data: image,
    });
  } catch (err) {
    console.error(err);
  }
};
export const deleteProduct = async (id) => {
  try {
    return await makeRequest({
      path: "/Product/" + id,
      method: "DELETE",
    });
  } catch (err) {
    console.error(err);
  }
};
export const deleteImage = async (id) => {
  try {
    return await makeRequest({
      path: "/Image/" + id,
      method: "DELETE",
    });
  } catch (err) {
    console.error(err);
  }
};
export const loadCategories = async () => {
  try {
    return await makeRequest({ path: "/Category" });
  } catch (err) {
    console.error(err);
  }
};
export const loadLocations = async () => {
  try {
    return await makeRequest({ path: "/Location" });
  } catch (err) {
    console.error(err);
  }
};

export const loadProductById = async (id) => {
  try {
    return await makeRequest({ path: "/Product/" + id });
  } catch (err) {
    console.error(err);
  }
};
