
import { useGetMyOrdersQuery } from "../../services/ordersService";
import { IMyOrder } from "../../types";
import MyOrder from "./MyOrderRow";
import MyOrdersHeader from "./MyOrdersHeader";

const  MyOrders = (): JSX.Element => {

  const {data: myOrders} = useGetMyOrdersQuery('')

    return (
      <main className="main">
         <section className="list-wrapper infoDetails">
          <MyOrdersHeader/>
           {myOrders?.map((myOrder: IMyOrder) => (
          <MyOrder myOrder={myOrder} key= {myOrder.id} />
          ))}
        {!myOrders || myOrders.length === 0 && <div className="item-row extend">No Pending orders</div>}

          </section>
      </main>
    );
  }
  
  export default MyOrders;