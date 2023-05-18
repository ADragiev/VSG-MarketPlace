import { BrowserRouter, Route, Routes } from "react-router-dom";
import Layout from "./components/Layout/layout";
import { Suspense, lazy } from "react";
import ProtectedRoute from "./protectedRoute";
// import './global/hamburger.tsx';



const Home = lazy(() => import('./pages/Home/home'));
const MarketPlace = lazy(() => import('./pages/Marketplace/marketplace'));
const Inventory = lazy(() => import('./pages/Inventory/inventory'));
const PendingOrders = lazy(() => import('./pages/Pending-Orders/pendingOrders'));
const MyOrders = lazy(() => import('./pages/My-Orders/myOrders'));



function App() {
  return (
    <>
<BrowserRouter>
<Suspense fallback={<div>Loading...</div>}>
        <Routes>
          <Route path="/" element={<Home/>}/>
           <Route element={<ProtectedRoute />}>
          <Route path="marketplace" element={<Layout><MarketPlace/></Layout>} />
          <Route path="inventory" element={<Layout><Inventory/></Layout>}/>
          <Route path="pending-orders" element={<Layout><PendingOrders/></Layout>} />
          <Route path="my-orders" element={<Layout><MyOrders/></Layout>} />
          </Route>
      </Routes>
      </Suspense>
</BrowserRouter>
</>
  );
}

export default App;
