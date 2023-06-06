import { BrowserRouter, Route, Routes } from "react-router-dom";
import Layout from "./components/Layout";
import { Suspense, lazy } from "react";
import ProtectedRoute from "./protectedRoute";
import AdminRoute from "./adminRoute";
import { CircularProgress } from "@mui/material";
import LentItems from "./pages/Lent-Items/LentItems";
import MyItems from "./pages/My-Items/MyItems";

const Home = lazy(() => import("./pages/Home/Home"));
const MarketPlace = lazy(() => import("./pages/Marketplace/Marketplace"));
const Inventory = lazy(() => import("./pages/Inventory/Inventory"));
const PendingOrders = lazy(
  () => import("./pages/Pending-Orders/PendingOrdersGrid")
);
const MyOrders = lazy(() => import("./pages/My-Orders/MyOrdersGrid"));

function App() {
  return (
    <>
      <BrowserRouter>
        <Suspense fallback={ <CircularProgress className="global-loader" />}>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route element={<ProtectedRoute />}>
              <Route
                path="marketplace"
                element={
                  <Layout>
                    <MarketPlace />
                  </Layout>
                }
              />
              <Route
                path="my-orders"
                element={
                  <Layout>
                    <MyOrders />
                  </Layout>
                }
              />
               <Route
                path="my-items"
                element={
                  <Layout>
                    <MyItems />
                  </Layout>
                }
              />
               
            </Route>

            <Route element={<AdminRoute />}>
              <Route
                path="inventory"
                element={
                  <Layout>
                    <Inventory />
                  </Layout>
                }
              />
             
               <Route
                path="pending-orders"
                element={
                  <Layout>
                    <PendingOrders />
                  </Layout>
                }
              />
               <Route
                path="lent-items"
                element={
                  <Layout>
                    <LentItems />
                  </Layout>
                }
              />
            </Route>

          </Routes>
        </Suspense>
      </BrowserRouter>
    </>
  );
}

export default App;
