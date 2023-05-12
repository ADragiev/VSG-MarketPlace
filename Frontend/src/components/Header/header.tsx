import { Link } from "react-router-dom";

const Header = (): JSX.Element => {
 const user = JSON.parse(sessionStorage.getItem('user')as string).name.split(' ')[0]
 
//  const name = result.user.account.name
 
  return (
    <header className="header">
      <Link to="/">
        <img
          src="../../images/vsg_marketplace-mini-logo 1.jpg"
          alt="mini-logo"
        />
      </Link>
      <span>Marketplace</span>
      <div id="greetingContainer" className="user">
        <span> Hi, {user}! </span>
        <img src="../../images/Profile Img.jpg" alt="Profile-pic" />
      </div>
      <div className="hamburger-icon-container">
        <div className="hamburger-lines">
          <input className="checkbox" type="checkbox" name="" id="" />
          <span className="line line1" />
          <span className="line line2" />
          <span className="line line3" />
        </div>
      </div>
    </header>
  );
};
export default Header;