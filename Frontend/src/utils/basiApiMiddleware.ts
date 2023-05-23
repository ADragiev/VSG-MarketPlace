import { AnyAction, Dispatch, isRejectedWithValue } from "@reduxjs/toolkit";
import { toast } from "react-toastify";
export const baseApiMiddleware = () => (next: Dispatch<AnyAction>) => (action: AnyAction) => {
  if (isRejectedWithValue(action)) {
    toast.error('Something went wrong. Please try again later');
  }
  return next(action);
}
