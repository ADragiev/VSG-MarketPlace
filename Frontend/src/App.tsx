import { BrowserRouter, Route, Routes } from "react-router-dom";
import Layout from "./components/Layout/layout";
import Home from "./pages/Home/home";
import MarketPlace from "./pages/Marketplace/marketplace";
import Inventory from "./pages/Inventory/inventory";
import MyOrders from "./pages/My-Orders/myOrders";
import PendingOrders from "./pages/Pending-Orders/pendingOrders";
// import './global/hamburger.tsx';

function App() {
  return (
<BrowserRouter>
<Routes>
          <Route path="/" element={<Home/>}/>
          <Route path="marketplace" element={<Layout><MarketPlace/></Layout>} />
          <Route path="inventory" element={<Layout><Inventory/></Layout>}/>
          <Route path="pending-orders" element={<Layout><PendingOrders/></Layout>} />
          <Route path="my-orders" element={<Layout><MyOrders/></Layout>} />
          
      </Routes>
</BrowserRouter>
  );
}

export default App;
