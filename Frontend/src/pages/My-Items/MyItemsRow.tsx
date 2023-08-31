import { IMyLentItem } from "types";

interface MyLentItemsRowProps {
  myLentItem: IMyLentItem;
}

const MyLentItemsRow = ({ myLentItem }: MyLentItemsRowProps) => {
  return (
    <div className="item-row extend">
      <span className="lentProductCode">{myLentItem.productCode}</span>
      <span className="lentProductName">{myLentItem.productName}</span>
      <span className="lentProductQty">{myLentItem.qty}</span>
      <span className="lentProductStartDate">{myLentItem.startDate}</span>
      <span className="lentProductEndDate">
        {myLentItem.endDate ? myLentItem.endDate : "-"}
      </span>
    </div>
  );
};

export default MyLentItemsRow;
