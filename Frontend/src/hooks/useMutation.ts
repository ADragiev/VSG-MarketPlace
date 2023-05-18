// import { useState } from 'react';
// import { makeRequest } from '../services/makeRequest';
// const useMutation = () => {
//   const [isLoading, setIsLoading] = useState(false);
//   const [error, setError] = useState(null);
//   const mutate = async ({ path, method, body }) => {
//     let data = null;
//     try {
//       setIsLoading(true);
//       const currentData = await makeRequest({ path, method, data: body });
//       data = currentData;
//     } catch (err) {
//       setError(err);
//     } finally {
//       setIsLoading(false);
//     }
//     return data;
//   }
//   return {
//     mutate,
//     isLoading,
//     error
//   }
// }
// export default useMutation