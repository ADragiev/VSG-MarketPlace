import {  msalInstance } from "../authConfig.ts";

export const handleLogout = async () => {
  await msalInstance.logoutRedirect({
    postLogoutRedirectUri: "/",
  });
  sessionStorage.clear();
};

