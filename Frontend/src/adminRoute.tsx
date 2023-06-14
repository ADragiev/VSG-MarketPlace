import { Navigate, Outlet } from "react-router-dom";

const AdminRoute = () => {
    const isAdmin = JSON.parse(sessionStorage.getItem('user')as string).memberType === 'Admin' 
    return isAdmin ? <Outlet /> : <Navigate to='not-authorized' />;
  }
  export default AdminRoute