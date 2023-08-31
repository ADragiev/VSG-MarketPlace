import { AuthenticationResult } from "@azure/msal-browser";
import { useNavigate } from "react-router-dom";
import { loginRequest, msalInstance } from "../authConfig";

const Login = () => {
  const navigate = useNavigate();

  const admitGroup = "f2123818-3d51-4fe4-990b-b072a80da143";
  let memberType = "User";

  const handleLogin = async () => {
    const result: AuthenticationResult = await msalInstance.loginPopup(
      loginRequest
    );
    const name = result.account?.name;
    const token = result.accessToken;
    const payload = JSON.parse(atob(token.split(".")[1])).groups;

    if (payload.includes(admitGroup)) {
      memberType = "Admin";
    }
    sessionStorage.setItem("user", JSON.stringify({ name, token, memberType }));

    navigate("/marketplace");
  };

  return (
    <a id="login" onClick={handleLogin}>
      LOGIN
    </a>
  );
};
export default Login;
