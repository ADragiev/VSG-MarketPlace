
// interface IRequestParams {
//   path: string;
//   method?: string;
//   data?: object;
//   headers?: Record<string, string>;
// }

// export const makeRequest = async <T>({
//   path,
//   method = "GET",
//   data = {},
//   headers = { "Content-Type": "application/json" },
// }: IRequestParams): Promise<T> => {
//   try {
//     const user = JSON.parse(sessionStorage.getItem('user')as string)
//     const options: { headers: Record<string, string>; body?: string } = {
//       headers: {...headers, Authorization: 'Bearer ' + user.token},
//     };

//     if (Object.keys(data).length > 0) {
//       options.body = JSON.stringify(data);
//     }

//     const res = await fetch(baseURL + path, {
//       method,
//       ...options,
//     });

//     if (!res.ok) {
//       throw new Error("Something went wrong!");
//     }
//     const contentType = res.headers.get("content-type");

//     return contentType ? await res.json() : res;
//   } catch (err) {
//     const error = err as T;
//     return error;
//   }
// };
