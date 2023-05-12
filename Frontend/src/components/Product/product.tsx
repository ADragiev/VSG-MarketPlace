import { useRef, useState } from "react";
import { IProduct } from "../../types";
import ProductModal from "./productModal";
import { Box, ClickAwayListener, Popper } from "@mui/material";

import { createOrder } from "../../services/itemsServices";

type ProductProps = {
  product: IProduct;
};
const Card = ({ product }: ProductProps): JSX.Element => {
  const [anchorEl, setAnchorEl] = useState();
  const open = Boolean(anchorEl);

  const options = [];
  for (let i = 1; i <= product.saleQty; i++) {
    options.push({ value: i });
  }

  const handlePopup = (e) => {
    setAnchorEl(e.currentTarget);
  };

  const [isModalOpen, setIsModalOpen] = useState(false);
  const selectValue = useRef(1);

  const onBuy = async () => {
    await createOrder(product.id, selectValue.current);
  };

  const handleOnImageClick = () => {
    setIsModalOpen(true);
  };

  const handleSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    selectValue.current = Number(event.target.value);
  };

  const arrow = {
    position: "relative",
    "&::before": {
      mt: "4px",
      backgroundColor: "white",
      content: '""',
      display: "block",
      position: "absolute",
      width: 10,
      height: 10,
      top: "-8px",
      zIndex: 99,
      transform: "rotate(45deg)",
      left: "-6px",
    },
  };

  return (
    <>
      {isModalOpen && (
        <ProductModal product={product} onClose={() => setIsModalOpen(false)} />
      )}
      <div className="card-item">
        <a className="product-image" onClick={handleOnImageClick}>
          <img
            src={
              product.image
                ? product.image
                : `./images/no_image-placeholder.png`
            }
            alt="ProductImage"
          />
        </a>
        <div className="details">
          <div className="name-price">
            <p>{product.price} BGN</p>
            <p>{product.category}</p>
          </div>
          <div className="details-wrapper">
            <div className="qty">
              <p>Qty</p>
              <select
                name="qty"
                className="selectQty"
                onChange={handleSelectChange}
              >
                {options.map((o) => (
                  <option value={o.value} key={o.value}>
                    {o.value}
                  </option>
                ))}
              </select>
            </div>

            <div className="icon popup">
              <a className="circle" id="firstBtn" onClick={handlePopup}>
                <img src="../../images/dollar.svg" alt="DollarImage" />
              </a>
            </div>
          </div>
        </div>
      </div>

      <Popper
        open={open}
        anchorEl={anchorEl}
        placement="bottom"
        disablePortal={false}
        modifiers={[
          {
            name: "flip",
            enabled: true,
            options: {
              altBoundary: true,
              rootBoundary: "document",
              padding: 8,
            },
          },
          {
            name: "preventOverflow",
            enabled: true,
            options: {
              altAxis: true,
              altBoundary: true,
              tether: true,
              rootBoundary: "document",
              padding: 8,
            },
          },
          {
            name: "arrow",
            enabled: true,
            options: {
              element: ".arrow",
            },
          },
        ]}
      >
        <Box component="span" className="arrow" sx={arrow}></Box>
        <ClickAwayListener onClickAway={() => setAnchorEl(null)}>
            <div className="popuptext">
              <span>
                Are you sure you want to buy{" "}
                <strong>{selectValue.current}</strong> item for{" "}
                <strong>{Number(selectValue.current) * product.price}</strong>?
              </span>
              <div className="buttons-container">
                <button onClick={onBuy} className="btnYesNo">
                  YES
                </button>
                <button onClick={() => setAnchorEl(null)} className="btnYesNo">
                  NO
                </button>
              </div>
            </div>
        </ClickAwayListener>
      </Popper>
      {/* <Popover
        open={open}
        anchorEl={anchorEl}
        onClose={() => setAnchorEl(null)}
        anchorOrigin={{
          vertical: "bottom",
          horizontal: "right",
        }}
        transformOrigin={{
          vertical: "top",
          horizontal: "center",
        }}
      >
        
        <div className="popuptext show">
          <span>
            Are you sure you want to buy <strong>{selectValue.current}</strong>{" "}
            item for{" "}
            <strong>{Number(selectValue.current) * product.price}</strong>?
          </span>
          <div className="buttons-container">
            <button onClick={onBuy} className="btnYesNo">
              YES
            </button>
            <button onClick={() => setAnchorEl(null)} className="btnYesNo">
              NO
            </button>
          </div>
        </div>
      </Popover> */}
      {/* {isModalOpen && (
        <ProductModal product={product} onClose={() => setIsModalOpen(false)} />
      )} */}
    </>
  );
};

export default Card;
