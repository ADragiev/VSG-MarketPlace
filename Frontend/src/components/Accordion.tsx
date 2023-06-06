import Accordion from "@mui/material/Accordion";
import AccordionSummary from "@mui/material/AccordionSummary";
import AccordionDetails from "@mui/material/AccordionDetails";
import Typography from "@mui/material/Typography";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import { ILentItem } from "types";
import { useReturnLentItemMutation } from "../services/lentItemsService";
import { toast } from "react-toastify";


interface AccordionProps {
  email: string;
  items: ILentItem[];
}

const AccordionComponent = ({ email, items }: AccordionProps) => {

    const [completeOrder] = useReturnLentItemMutation();

    const handleCompleteOrder = async (id: number) => {
        const response = await completeOrder(id)
        if (!('error' in response)) {
            toast.success("Successfully returned item");
          } 
    }

    
  return (
    <Accordion sx={{ width: "94%"}}>
      <AccordionSummary
        expandIcon={<ExpandMoreIcon />}
        aria-controls="panel1a-content"
        id="panel1a-header"
      >
        <Typography>{email}</Typography>
      </AccordionSummary>
      <AccordionDetails>
        <>
          <div className="header-row">
            <span className="LentProductCode">Product Code</span>
            <span className="LentProductName">Product Name</span>
            <span className="LentProductQty">QTY</span>
            <span className="LentProductStartDate">Start date</span>
            <span className="LentProductEndDate">End date </span>
            <span className="LentBtn">Action </span>

          </div>

          {items?.map((i: ILentItem) => (
             <div className="item-row extend">
             <span className="LentProductCode">{i.productCode}</span>
             <span className="LentProductName">{i.productName}</span>
             <span className="LentProductQty">{i.qty}</span>
             <span className="LentProductStartDate">{i.startDate}</span>
             <span className="LentProductEndDate">{i.endDate? i.endDate : '-'}</span>
             {!i.endDate && (
            //    <a className="">
            //      <svg
            //        width={12}
            //        height={12}
            //        viewBox="0 0 12 12"
            //        fill="none"
            //        xmlns="http://www.w3.org/2000/svg"
            //      >
            //        <path
            //          d="M11.8203 1.35156L7.17188 6L11.8203 10.6484L10.6484 11.8203L6 7.17188L1.35156 11.8203L0.179688 10.6484L4.82812 6L0.179688 1.35156L1.35156 0.179688L6 4.82812L10.6484 0.179688L11.8203 1.35156Z"
            //          fill="#ED1C25"
            //        />
            //      </svg>
            //    </a>
             <button className="btnColumn completeBtn" onClick= {()=> handleCompleteOrder(i.id)}>Complete</button>

             )}
           </div>
          ))}
         
        </>
      </AccordionDetails>
    </Accordion>
  );
};

export default AccordionComponent;
