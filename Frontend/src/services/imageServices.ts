import { makeRequest } from "./makeRequest";

export const postImageById = async (id: number, image: FormData) => {
  const user = JSON.parse(sessionStorage.getItem('user')as string)
  console.log(user.token);
  

  try {
    return await fetch(`https://localhost:7054/Image/${id}`, {
      method: "POST",
      headers: {
        "Authorization": 'Bearer ' + user.token
      },
      body: image
    });
  } catch (error) {
    console.log(error);
  }
};

export const deleteImage = async (id: number) => {
  return fetch(`https://localhost:7054/Image/${id}`, {
    method: "DELETE",
  });
};
