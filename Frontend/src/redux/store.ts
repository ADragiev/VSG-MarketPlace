import { configureStore } from "@reduxjs/toolkit";
import { baseApi } from "../utils/baseApi";
import { baseApiMiddleware } from "../utils/basiApiMiddleware";

 const store = configureStore({
  reducer: {
    [baseApi.reducerPath]: baseApi.reducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(baseApi.middleware, baseApiMiddleware),
  devTools:true
})
 export default store;
