import { ReactNode } from "react";
import Header from "./Header";
import Sidebar from "./Sidebar";

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
