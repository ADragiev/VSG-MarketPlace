import { AnyAction, Dispatch, isRejectedWithValue } from "@reduxjs/toolkit";
import { toast } from "react-toastify";
export const baseApiMiddleware = () => (next: Dispatch<AnyAction>) => (action: AnyAction) => {
  if (isRejectedWithValue(action as AnyAction)) {
    if (action.payload.status===400) {
      toast.error(action.payload.data[0].title);
    }
    else{
      toast.error('Something went wrong. Please try again later');
    }
    if (action.payload.status === 401) {
      window.location.replace('/')
      toast.error('Session expired. Please login again');
    }
  }
  
   
  return next(action);
}