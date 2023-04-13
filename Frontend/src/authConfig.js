import { PublicClientApplication } from '@azure/msal-browser';

export const msalConfig = {
    auth: {
        clientId: "aaf457df-aabd-45a6-8df0-7930b111c6fe",
        authority: "https://login.microsoftonline.com/50ae1bf7-d359-4aff-91ac-b084dc52111e",
        redirectUri: "http://localhost:3000",
    },
    cache: {
        cacheLocation: "sessionStorage", 
        storeAuthStateInCookie: false, 
    }
};

export const loginRequest = {
    scopes: ["User.Read"]
};

export const msalInstance = new PublicClientApplication(msalConfig);
