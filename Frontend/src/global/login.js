import { loginRequest, msalInstance } from "../authConfig.js";

document.querySelector("#login").addEventListener("click", async  () => {
 let login =  await msalInstance.loginPopup(loginRequest);
 let response = login.account
 sessionStorage.setItem('user', JSON.stringify(response))
 window.location.href = "/marketplace.html";
});
