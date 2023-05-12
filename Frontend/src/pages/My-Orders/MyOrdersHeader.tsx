const MyOrdersHeader = (): JSX.Element => {
    return (
      <div className="header-row">
        <span className="ProductNameColumn">Name</span>
        <span className="ProductQtyColumn">QTY</span>
        <span className="ProductPriceColumn">Price</span>
        <span className="ProductDateColumn">Order Date</span>
        <span className="ProductStatus">Status </span>
      </div>
    );
  };
  
  export default MyOrdersHeader;
  