const PendingOrderHeader = (): JSX.Element => {
  return (
    <div className="header-row">
      <span className="codeColumn">Code</span>
      <span className="qtyColumn">QTY</span>
      <span className="priceColumn">Price</span>
      <span className="emailColumn">Ordered By</span>
      <span className="dateColumn">Order Date</span>
      <span className="btnColumn">Action</span>
    </div>
  );
};

export default PendingOrderHeader;
