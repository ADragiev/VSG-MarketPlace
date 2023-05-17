
import { useGetMyOrdersQuery } from "../../services/ordersService";
import MyOrder from "./MyOrder";
import MyOrdersHeader from "./MyOrdersHeader";

const  MyOrders = (): JSX.Element => {

  const {data: myOrders} = useGetMyOrdersQuery('')
  
    return (
      <main className="main">
         <section className="list-wrapper infoDetails">
          <MyOrdersHeader/>
        {myOrders?.map((myOrder) => (
          <MyOrder myOrder={myOrder} key= {myOrder.id} />
          ))}
          </section>
      </main>
    );
  }
  
  export default MyOrders;