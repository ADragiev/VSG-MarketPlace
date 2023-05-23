
import Login from "../../auth/Login";

function Home() {

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
          <Login/>
      </div>
    </main>
  );
}

export default Home;
