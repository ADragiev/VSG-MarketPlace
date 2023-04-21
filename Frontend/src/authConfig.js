import { PublicClientApplication } from "@azure/msal-browser/dist/index.js";

export const msalConfig = {
  auth: {
    clientId: "a43d36a8-e799-4be7-a149-203b43f968db",
    authority:
      "https://login.microsoftonline.com/50ae1bf7-d359-4aff-91ac-b084dc52111e",
  },
  cache: {
    cacheLocation: "sessionStorage",
    storeAuthStateInCookie: false,
  },
};

export const loginRequest = {
  scopes: ["User.Read"],
};

export const msalInstance = new PublicClientApplication(msalConfig);
