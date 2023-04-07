import { makeRequest } from "../src/makeRequest.js";


export const loadProducts = async () => {
  try {
    const data = await makeRequest({ path: "/products" });
    return data;
  } catch (err) {
    console.error(err);
  }
};

export const loadProductById = async (id) => {
  try {
    const data = await makeRequest({ path: "/products/" + id });
    return data;
  } catch (err) {
    console.error(err);
  }
};


