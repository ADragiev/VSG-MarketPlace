import {  useGetPendingOrdersQuery } from "../../services/ordersService";
import { IPendingOrder } from "../../types";
import PendingOrder from "./PendingOrder";
import PendingOrdersHeader from "./PendingOrdersHeader";

const PendingOrders = (): JSX.Element => {

  const {data: pendingOrders} = useGetPendingOrdersQuery('')

  return (
    <main className="main">
      <section className="list-wrapper infoDetails">
      <PendingOrdersHeader />
        {pendingOrders?.map((pendingOrder: IPendingOrder) => (
          <PendingOrder pendingOrder={pendingOrder} key={pendingOrder.id} />
        ))}
      </section>
    </main>
  );
};

export default PendingOrders;
