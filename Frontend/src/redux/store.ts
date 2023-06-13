import { configureStore } from "@reduxjs/toolkit";
import { baseApi } from "../utils/baseApi";
import { baseApiMiddleware } from "../utils/baseApiMiddleware";
import { baseEmployeesApi } from "../utils/baseEmployeesApi";

 const store = configureStore({
  reducer: {
    [baseApi.reducerPath]: baseApi.reducer,
    [baseEmployeesApi.reducerPath]: baseEmployeesApi.reducer
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(baseApi.middleware,baseEmployeesApi.middleware, baseApiMiddleware),
  devTools:true
})
 export default store;
