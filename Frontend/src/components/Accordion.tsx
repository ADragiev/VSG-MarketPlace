import { useState } from "react";
import AccordionDetails from "@mui/material/AccordionDetails";
import { ILentItem } from "types";
import { useReturnLentItemMutation } from "../services/lentItemsService";
import { toast } from "react-toastify";


interface AccordionProps {
  lentItem: ILentItem;
}

const AccordionComponent = ({lentItem}: AccordionProps) => {

    const [completeOrder] = useReturnLentItemMutation();
    const [currentEndDate, setCurrentEndDate] = useState(lentItem.endDate)
  
    const handleCompleteOrder = async ( id: number) => {
        const response = await completeOrder(id)
        
        if (!('error' in response)) {
            setCurrentEndDate(response.data.returnedValue)
            toast.success("Successfully returned item");
          } 
    }    
  return (
   
     <AccordionDetails>
     <>
          <div className="item-row extend">
          <span className="LentProductCode">{lentItem.productCode}</span>
          <span className="LentProductName">{lentItem.productName}</span>
          <span className="LentProductQty">{lentItem.qty}</span>
          <span className="LentProductStartDate">{lentItem.startDate}</span>
          <span className="LentProductEndDate">{currentEndDate? currentEndDate :  <button className="btnColumn returnBtn" onClick= {()=> handleCompleteOrder( lentItem.id)}>Return</button>}</span>
        </div>
      
     </>
   </AccordionDetails>
  );
};

export default AccordionComponent;
