import { loginRequest, msalInstance } from "../authConfig.js";

document.querySelector("#login").addEventListener("click", async  () => {
  const result = await msalInstance.loginRedirect(loginRequest);

  console.log(result);
});
