import { useState } from "react";
import { IMyOrder } from "../../types";
import Popover from "@mui/material/Popover";
import Box from "@mui/material/Box";
import { ClickAwayListener, Popper } from "@mui/material";
import { rejectOrder } from "../../services/itemsServices";

type MyOrderProps = {
    myOrder: IMyOrder
}

function MyOrder({myOrder}: MyOrderProps) {

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

  const [anchorEl, setAnchorEl] = useState();
  const open = Boolean(anchorEl);


  const handlePopup = (e) => {
    setAnchorEl(e.currentTarget);
  };

  const onReject = async () => {
    await rejectOrder(myOrder.id);
  };
    return (
        <div className="item-row extend">
        <span className="ProductNameColumn">
          {myOrder.productName}
        </span>
        <span className="ProductQtyColumn">
          {myOrder.qty}
        </span>
        <span className="ProductPriceColumn">
          {myOrder.price} BGN
        </span>
        <span className="ProductDateColumn">
          {myOrder.date}
        </span>
          <span className="status">
            {myOrder.status}
          </span>
         {
           myOrder.status  == "Pending" &&
          <a className="deleteIcon" onClick={handlePopup}>
            <svg
              width={12}
              height={12}
              viewBox="0 0 12 12"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M11.8203 1.35156L7.17188 6L11.8203 10.6484L10.6484 11.8203L6 7.17188L1.35156 11.8203L0.179688 10.6484L4.82812 6L0.179688 1.35156L1.35156 0.179688L6 4.82812L10.6484 0.179688L11.8203 1.35156Z"
                fill="#ED1C25"
              />
            </svg>
            {/* {open && (<Box
          sx={{
            position: "relative",
            "&::before": {
              boxShadow: 1,
              mt: '4px',
              backgroundColor: "white",
              content: '""',
              display: "block",
              position: "absolute",
              width: 10,
              height: 10,
              zIndex: 99,
              top: '-8px',
              transform: "rotate(45deg)",
              left: "1px",
            },
          }}
        />)} */}
          </a>

         }
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
              <span> Are you sure you want to reject this order? </span>
              <div className="buttons-container">
                <button onClick={onReject} className="btnYesNo">YES</button>
                <button onClick={() => setAnchorEl(null)} className="btnYesNo">NO</button>
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
        
        <div className="popuptext">
              <span> Are you sure you want to reject this order? </span>
              <div className="buttons-container">
                <button className="btnYesNo">YES</button>
                <button className="btnYesNo">NO</button>
              </div>
            </div>
      </Popover> */}
      </div>      
      
    );
  }
  
  export default MyOrder;