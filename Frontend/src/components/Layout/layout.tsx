import { ReactNode } from "react";
import Header from "../Header/header";
import Sidebar from "../Sidebar/sidebar";

type Props = {
    children: ReactNode
}

const Layout = ({ children }: Props) => {
  return (
    <>
      <Header />
      <div className="container">
        <Sidebar />
          {children}
      </div>
    </>
  );
};

export default Layout;
