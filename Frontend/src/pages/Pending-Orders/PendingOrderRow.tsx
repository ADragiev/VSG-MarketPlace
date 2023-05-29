import { toast } from "react-toastify";
import { useConfirmOrderMutation } from "../../services/ordersService";
import { IPendingOrder } from "../../types";
import { useRef } from "react";

type PendingOrderProps = {
    pendingOrder: IPendingOrder
}



const PendingOrderRow = ({pendingOrder}: PendingOrderProps) => {


  const [completeOrder] = useConfirmOrderMutation();
  const orderRef = useRef<HTMLDivElement>(null)

    const onComplete = async () => {
       const response =  await completeOrder(pendingOrder.id);
     if (!('error' in response)) {
      orderRef.current?.remove()
       toast.success('Successfully completed order')
     }
      };
    return (
        <div ref={orderRef} className="item-row">
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
  
  export default PendingOrderRow;