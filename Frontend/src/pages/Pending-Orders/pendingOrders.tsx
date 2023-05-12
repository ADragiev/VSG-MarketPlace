import { useEffect, useState } from "react";
import { loadPendingOrders } from "../../services/itemsServices";
import { IPendingOrder } from "../../types";
import PendingOrder from "./PendingOrder";
import PendingOrdersHeader from "./PendingOrdersHeader";

const PendingOrders = (): JSX.Element => {
  const [pendingOrders, setPendingOrders] = useState<IPendingOrder[]>([]);

  useEffect(() => {
    const resultFunc = async () => {
      const result: IPendingOrder[] = await loadPendingOrders();
      setPendingOrders(result);
    };
    resultFunc();
  }, []);

  return (
    <main className="main">
      <section className="list-wrapper infoDetails">
      <PendingOrdersHeader />
        {pendingOrders.map((pendingOrder) => (
          <PendingOrder pendingOrder={pendingOrder} key={pendingOrder.id} />
        ))}
      </section>
    </main>
  );
};

export default PendingOrders;
