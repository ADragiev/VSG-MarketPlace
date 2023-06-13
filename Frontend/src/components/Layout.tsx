import { ReactNode } from "react";
import Header from "./Header";
import Sidebar from "./Sidebar";
import { useGetEmployeesQuery } from "../utils/baseEmployeesApi";
import { IEmployee } from "types";

type Props = {
  children: ReactNode;
};

const Layout = ({ children }: Props) => {
  const user = JSON.parse(sessionStorage.getItem("user") as string);

  const { data: employees } = useGetEmployeesQuery();
  const currentUser = employees?.find(
    (e: IEmployee) => e.name.toLowerCase() === user.name.toLowerCase()
  );

  const modifiedUser = {
    ...user,
    avatar: currentUser?.avatar,
  };

  return (
    <>
      <Header currentUser={currentUser} />
      <div className="container">
        <Sidebar currentUser={modifiedUser} />
        {children}
      </div>
    </>
  );
};

export default Layout;
