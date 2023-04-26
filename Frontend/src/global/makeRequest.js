const baseURL = "https://localhost:7054";

export const makeRequest = async ({
  path,
  method = "GET",
  data = {},
  headers = {}
}) => {
  try {
    const token = localStorage.getItem("token");

    const options = {
      headers: {
        Authorization: "Bearer " + token,
        "Content-Type": "application/json",
        ...headers
      }
    };

    if (Object.keys(data).length > 0) {
      options.body = JSON.stringify(data);
    }

    const res = await fetch(baseURL + path, {
      method,
      ...options
    });

    if (!res.ok) {
      return Promise.reject("Something went wrong!");
    }

    return res ? await res.json(): res;
  } catch (err) {
    throw Error(err);
  }
};
export const postImage = async (id, image) => {
  let response = await fetch(baseURL + '/Image/' + id,{
    method: "POST",
    body: image,
  })
  return response;
};
