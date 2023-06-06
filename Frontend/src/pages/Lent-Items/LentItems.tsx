import { useGetLentItemsQuery } from "../../services/lentItemsService";
import AccordionComponent from "../../components/Accordion";
import Typography from "@mui/material/Typography";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import { Accordion, CircularProgress } from "@mui/material";
import AccordionSummary from "@mui/material/AccordionSummary";

import { IUserLendItem } from "types";

const LentItems = (): JSX.Element => {
  const { data: users, isLoading } = useGetLentItemsQuery("fds");

  return (
    <main className="main">
      <section className="list-wrapper infoDetails">
        {isLoading ? (
          <CircularProgress className="marketplace-loader" />
        ) : (
          users?.map((user: IUserLendItem) => (
            <Accordion sx={{ width: "94%" }}>
              <AccordionSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel1a-content"
                id="panel1a-header"
              >
                <img
                  className="profilePicAccordion"
                  src="../../images/Profile Img.jpg"
                  alt="Profile-pic"
                />
                <Typography>{user.email}</Typography>
              </AccordionSummary>
              <div className="lent-items-header">
                <span className="LentProductCode">Product Code</span>
                <span className="LentProductName">Product Name</span>
                <span className="LentProductQty">QTY</span>
                <span className="LentProductStartDate">Start date</span>
                <span className="LentProductEndDate">End date </span>
              </div>
              {user.lentItems.map((i) => (
                <AccordionComponent i={i} />
              ))}
            </Accordion>
          ))
        )}
      </section>
    </main>
  );
};

export default LentItems;
