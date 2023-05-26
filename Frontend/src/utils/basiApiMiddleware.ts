import { AnyAction, Dispatch, isRejectedWithValue } from "@reduxjs/toolkit";
import { toast } from "react-toastify";
export const baseApiMiddleware = () => (next: Dispatch<AnyAction>) => (action: AnyAction) => {
  if (isRejectedWithValue(action as AnyAction)) {
    toast.error('Something went wrong. Please try again later');
    if (action.payload.status === 401) {
      window.location.replace('/')
    }
  }
  return next(action);
}
