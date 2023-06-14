import { useGetLentItemsQuery } from "../../services/lentItemsService";
import AccordionComponent from "../../components/Accordion";
import Typography from "@mui/material/Typography";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import { Accordion, Avatar, CircularProgress } from "@mui/material";
import AccordionSummary from "@mui/material/AccordionSummary";

import { IEmployee, IModifiedUser, IUserLendItem } from "types";
import { useGetEmployeesQuery } from "../../utils/baseEmployeesApi";

const LentItems = (): JSX.Element => {
  const { data: users, isLoading } = useGetLentItemsQuery();
  const { data: employees } =  useGetEmployeesQuery();

  const modifiedUsers = users?.map((user: IUserLendItem) => {
    const avatar = employees?.find((e : IEmployee) => e.email.toLowerCase() === user.email.toLowerCase())?.avatar
    const name = employees?.find((e : IEmployee)=> e.email.toLowerCase() === user.email.toLowerCase())?.name

    return ({
      ...user,
      avatar,
      name
    }
    )
  })
  

  return (
    <main className="main">
      <section className="list-wrapper infoDetails">
        {isLoading ? (
          <CircularProgress className="marketplace-loader" />
        ) : (
          modifiedUsers?.map((user: IModifiedUser) => (
            <Accordion className="accordion">
              <AccordionSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel1a-content"
                id="panel1a-header"
              >
                <Avatar className="profilePicAccordion" alt="Avatar" src={user.avatar} />
              
                <Typography>{user.name}</Typography>
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
