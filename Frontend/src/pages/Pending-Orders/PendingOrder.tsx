import { useConfirmOrderMutation } from "../../services/ordersService";
import { IPendingOrder } from "../../types";

type PendingOrderProps = {
    pendingOrder: IPendingOrder
}



const PendingOrder = ({pendingOrder}: PendingOrderProps) => {


  const [completeOrder] = useConfirmOrderMutation();

    const onComplete = async () => {
        await completeOrder(pendingOrder.id);
      };
    return (
        <div className="item-row">
        <div className="div-wrapper">
          <span className="codeColumn">
            {pendingOrder.productCode}
          </span>
          <span className="qtyColumn">
            {pendingOrder.qty}
          </span>
          <span className="priceColumn">
            {pendingOrder.price} BGN
          </span>
        </div>
        <span className="emailColumn">
          {pendingOrder.orderedBy}
        </span>
        <span className="dateColumn">
          {pendingOrder.date}
        </span>
        <button className="btnColumn completeBtn" onClick= {onComplete}>Complete</button>
      </div>
      
    );
  }
  
  export default PendingOrder;