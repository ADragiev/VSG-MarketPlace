import { useEffect, useState } from "react";
import { IMyOrder } from "../../types";
import { loadMyOrders } from "../../services/itemsServices";
import MyOrder from "./MyOrder";
import MyOrdersHeader from "./MyOrdersHeader";

const  MyOrders = (): JSX.Element => {


  const [myOrders, setMyOrders] = useState<IMyOrder[]>([]);

  useEffect(() => {
    const resultFunc = async () => {
      const result: IMyOrder[] = await loadMyOrders();
      setMyOrders(result);
    };
    resultFunc();
  }, []);
 

    return (
      <main className="main">
         <section className="list-wrapper infoDetails">
          <MyOrdersHeader/>
        {myOrders.map((myOrder) => (
          <MyOrder myOrder={myOrder} key= {myOrder.id} />
          ))}
          </section>
      </main>
    );
  }
  
  export default MyOrders;