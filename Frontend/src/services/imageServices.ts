import { baseApi } from "../utils/baseApi";


const PostImage = "postImage";
const DeleteImage = "deleteImage"

const imageServices = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    [PostImage]: builder.mutation({
      query: ({id, imageFormData}) => ({
        method: "POST",
        url: `/Image/${id}`,
        body: imageFormData
      }),
    }),
    [DeleteImage]: builder.mutation({
      query: (id) => ({
        method: "DELETE",
        url: `/Image/${id}`,
      }),
    }),
  }),
});

export const {
usePostImageMutation,
useDeleteImageMutation
} = imageServices;
