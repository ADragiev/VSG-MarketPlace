import { isRejectedWithValue } from "@reduxjs/toolkit";
import { toast } from "react-toastify";
export const baseApiMiddleware = () => (next) => (action) => {
  if (isRejectedWithValue(action)) {
    toast.error(action.payload);
  }
  return next(action);
}