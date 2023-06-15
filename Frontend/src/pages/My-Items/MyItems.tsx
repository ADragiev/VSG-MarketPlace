import { useGetMyLentItemsQuery } from "../../services/lentItemsService";
import MyItemsHeader from "./MyItemsHeader";
import { CircularProgress } from "@mui/material";
import { IMyLentItem } from "types";
import MyLentItemsRow from "./MyItemsRow";

const MyItems = (): JSX.Element => {
  const { data: myLentItems, isLoading } = useGetMyLentItemsQuery();

  return (
    <main className="main">
      <section className="list-wrapper infoDetails">
        <MyItemsHeader />
        {isLoading ? (
          <CircularProgress className="myOrders-loader" />
        ) : (
          myLentItems?.map((myLentItem: IMyLentItem) => (
            <MyLentItemsRow myLentItem={myLentItem} />
          ))
        )}
      </section>
    </main>
  );
};

export default MyItems;
