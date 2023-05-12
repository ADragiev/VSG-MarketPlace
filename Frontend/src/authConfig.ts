import { PublicClientApplication } from "@azure/msal-browser/dist/index.js";

export const msalConfig = {
  auth: {
    clientId: "86ceffd4-8632-4677-bbb6-e7badafa26ec",
    authority:
      "https://login.microsoftonline.com/50ae1bf7-d359-4aff-91ac-b084dc52111e",
  },
  cache: {
    cacheLocation: "sessionStorage",
    storeAuthStateInCookie: false,
  },
};

export const loginRequest = {
  scopes: ["email profile openid api://86ceffd4-8632-4677-bbb6-e7badafa26ec/Files.Read"],
};

export const msalInstance = new PublicClientApplication(msalConfig);