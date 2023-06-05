
import { CircularProgress } from "@mui/material";
import { useGetMyOrdersQuery } from "../../services/ordersService";
import { IMyOrder } from "../../types";
import MyOrder from "./MyOrderRow";
import MyOrdersHeader from "./MyOrdersHeader";

const  MyOrders = (): JSX.Element => {

  const {data: myOrders, isLoading} = useGetMyOrdersQuery('')

    return (
      <main className="main">
         <section className="list-wrapper infoDetails">
          <MyOrdersHeader/>
          {isLoading? <CircularProgress className="myOrders-loader"/> :  myOrders?.map((myOrder: IMyOrder) => (
          <MyOrder myOrder={myOrder} key= {myOrder.id} />
          ))}
        {!myOrders && !isLoading && <div className="item-row extend">No Pending orders</div>}

          </section>
      </main>
    );
  }
  
  export default MyOrders;