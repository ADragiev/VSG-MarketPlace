import { useEffect } from "react";
import { Link, useLocation } from "react-router-dom";

const Header = (): JSX.Element => {
  const user = JSON.parse(sessionStorage.getItem("user") as string).name.split( " ")[0];
  const root = document.querySelector("#root") as HTMLElement;

  const location = useLocation();
  const title = location.pathname
    .slice(1)
    .split("-")
    .map((w) => {
      const firstLetter = w.charAt(0).toUpperCase();
      const restOfWord = w.slice(1);
      return `${firstLetter}${restOfWord}`;
    });

  const handleInputChange = () => {
    if (root.className == "onHamburger") {
      root.className = "onClose";
    } else {
      root.className = "onHamburger";
    }
  };

  useEffect(() => {
    if (window.innerWidth >= 769) {
      root.className = "onClose";
    } else{
       const checkbox = document.querySelector('.checkbox') as HTMLInputElement
       checkbox.checked = false
      root.className = "";
    }
  }, [location.pathname]);

  //  const name = result.user.account.name

  return (
    <header className="header" role='header'>
      <Link to="/">
        <img
          src="../../images/vsg_marketplace-mini-logo 1.jpg"
          alt="mini-logo"
        />
      </Link>
      <span>{title.join(" ")}</span>
      <div id="greetingContainer" className="user">
        <span> Hi, {user}! </span>
        <img src="../../images/Profile Img.jpg" alt="Profile-pic" />
      </div>
      <div className="hamburger-icon-container">
        <div className="hamburger-lines">
          <input
            onInput={handleInputChange}
            className="checkbox"
            type="checkbox"
            name=""
            id=""
          />
          <span className="line line1" />
          <span className="line line2" />
          <span className="line line3" />
        </div>
      </div>
    </header>
  );
};
export default Header;
