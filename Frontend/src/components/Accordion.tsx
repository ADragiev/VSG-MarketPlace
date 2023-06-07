import AccordionDetails from "@mui/material/AccordionDetails";
import { ILentItem } from "types";
import { useReturnLentItemMutation } from "../services/lentItemsService";
import { toast } from "react-toastify";
import { useState } from "react";


interface AccordionProps {
  i: ILentItem;
}

const AccordionComponent = ({i}: AccordionProps) => {

    const [completeOrder] = useReturnLentItemMutation();
    const [currentEndDate, setCurrentEndDate] = useState(i.endDate)
  
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
          <span className="LentProductCode">{i.productCode}</span>
          <span className="LentProductName">{i.productName}</span>
          <span className="LentProductQty">{i.qty}</span>
          <span className="LentProductStartDate">{i.startDate}</span>
          <span className="LentProductEndDate">{currentEndDate? currentEndDate :  <button className="btnColumn completeBtn" onClick= {()=> handleCompleteOrder( i.id)}>Complete</button>}</span>
        </div>
      
     </>
   </AccordionDetails>
  );
};

export default AccordionComponent;
