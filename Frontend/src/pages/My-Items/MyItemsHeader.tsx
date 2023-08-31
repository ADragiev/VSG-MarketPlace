const MyItemsHeader = (): JSX.Element => {
  return (
    <div className="header-row">
      <span className="productCode">Product code</span>
      <span className="productName">Product name</span>
      <span className="lentQtyColumn">QTY</span>
      <span className="startDate">Start date</span>
      <span className="endDate">End date</span>
    </div>
  );
};

export default MyItemsHeader;
