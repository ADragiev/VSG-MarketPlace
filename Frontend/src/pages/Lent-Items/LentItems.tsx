import { useGetLentItemsQuery } from "../../services/lentItemsService";
import AccordionComponent from "../../components/Accordion";
import { CircularProgress } from "@mui/material";
import { IUserLendItem } from "types";

const LentItems = (): JSX.Element => {
  const { data: users, isLoading} = useGetLentItemsQuery("fds");

  
  return (
    <main className="main">
      <section className="list-wrapper infoDetails">
      {isLoading ? <CircularProgress className="marketplace-loader"/> :
      users?.map((user: IUserLendItem) =>(
      <AccordionComponent email={user.email} items={user.lentItems} />
      ))}
    
      </section>
    </main>
  );
};

export default LentItems;
