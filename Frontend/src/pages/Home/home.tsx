import { useNavigate } from "react-router-dom";
import { AuthenticationResult } from "@azure/msal-browser";
import { loginRequest, msalInstance } from "../../authConfig";




function Home() {


  const navigate = useNavigate()

   const handleLogin = async () => {
    const result: AuthenticationResult = await msalInstance.loginPopup(
      loginRequest
    );
    const name = result.account?.name;
    const token = result.accessToken;
    sessionStorage.setItem("user", JSON.stringify({ name, token }));
    navigate('/marketplace')
    
  };

  sessionStorage.clear()
  return (
    <main className="mainContainer">
      <div className="divs">
        <img
          id="mainLogo"
          src="images/vsg_marketplace_logo_2.png"
          alt="vsgLogo"
        />
      </div>
      <div className="divs">
          <a id="login" onClick={handleLogin}>
            LOGIN 
          </a>
      </div>
    </main>
  );
}

export default Home;
