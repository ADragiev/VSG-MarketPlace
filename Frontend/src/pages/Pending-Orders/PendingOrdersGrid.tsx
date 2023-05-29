import {  useGetPendingOrdersQuery } from "../../services/ordersService";
import { IPendingOrder } from "../../types";
import PendingOrderRow from "./PendingOrderRow";
import PendingOrdersHeader from "./PendingOrdersHeader";

const PendingOrders = (): JSX.Element => {

  const {data: pendingOrders} = useGetPendingOrdersQuery('')

  return (
    <main className="main">
      <section className="list-wrapper infoDetails">
      <PendingOrdersHeader />
        {pendingOrders?.map((pendingOrder: IPendingOrder) => (
          <PendingOrderRow pendingOrder={pendingOrder} key={pendingOrder.id} />
        ))}
        {!pendingOrders || pendingOrders.length === 0 && <div className="item-row">No Pending orders</div>}
      </section>
    </main>
  );
};

export default PendingOrders;
