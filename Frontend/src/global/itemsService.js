import { makeRequest } from "./makeRequest.js";

export const loadProducts = async () => {
  try {
    const data = await makeRequest({ path: "/Product" });
    return data;
  } catch (err) {
    console.error(err);
  }
};
export const loadInventoryItems = async () => {
  try {
    const data = await makeRequest({ path: "/Product/Inventory" });
    return data;
  } catch (err) {
    console.error(err);
  }
};
export const loadProductsDetails = async (id) => {
  try {
    const data = await makeRequest({ path: "/Product/Details/" + id });
    return data;
  } catch (err) {
    console.error(err);
  }
};

export const loadPendingOrders = async () => {
  try {
    const data = await makeRequest({ path: "/Order" });
    return data;
  } catch (err) {
    console.error(err);
  }
};
export const postImageById = async (id, image) => {
  try {
    const data = await makeRequest({
      path: "/Image/" + id,
      method: "POST",
      data: image,
    });
    console.log(data);
    return data;
  } catch (err) {
    console.error(err);
  }
};
export const deleteProduct = async (id) => {
  try {
    const data = await makeRequest({
      path: "/Product/" + id,
      method: "DELETE",
    });
    return data;
  } catch (err) {
    console.error(err);
  }
};
export const deleteImage = async (id) => {
  try {
    const data = await makeRequest({
      path: "/Image/" + id,
      method: "DELETE",
    });
    return data;
  } catch (err) {
    console.error(err);
  }
};
export const loadCategories = async () => {
  try {
    const data = await makeRequest({ path: "/Category" });
    return data;
  } catch (err) {
    console.error(err);
  }
};
export const loadLocations = async () => {
  try {
    const data = await makeRequest({ path: "/Location" });
    return data;
  } catch (err) {
    console.error(err);
  }
};

export const loadProductById = async (id) => {
  try {
    const data = await makeRequest({ path: "/Product/" + id });
    return data;
  } catch (err) {
    console.error(err);
  }
};
