import styled from "@emotion/styled";

import { deleteProduct } from "../../services/itemsServices";
import EditItemForm from "../../components/editItemForm";
import { useState,useRef } from "react";
import { IInventoryItem } from "../../types";
import { Box, ClickAwayListener, Popper, TableCell, TableRow, tableCellClasses } from "@mui/material";

type InventoryItemsProps = {
  product: IInventoryItem;
};

const TableRowComponent = ({ product }: InventoryItemsProps): JSX.Element => {

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
  
  const [anchorEl, setAnchorEl] = useState(null);
  const open = Boolean(anchorEl);
  const handlePopup = (e) => {
    e.preventDefault();
    setAnchorEl(e.currentTarget);
    
  };

  const [isEditItemFormOpen, setIsEditItemFormOpen] = useState(false);

  const handleEditItemBtn = () => {
    setIsEditItemFormOpen(true);
  };

  const onDelete = async () => {
    await deleteProduct(product.id);
  };

  const StyledTableCell = styled(TableCell)(() => ({
    [`&.${tableCellClasses.head}`]: {
      fontWeight: 600,
      border: 1,
      fontSize: 17,
    },
    [`&.${tableCellClasses.body}`]: {
      fontWeight: 500,
      fontSize: 16,
    },
  }));

  const StyledTableRow = styled(TableRow)(() => ({
    // hide last border
  }));

  return (
    <>
      {isEditItemFormOpen && (
        <EditItemForm
          onClose={() => setIsEditItemFormOpen(false)}
          product={product}
        />
      )}
      <StyledTableRow key={product.id}>
        <StyledTableCell component="th" scope="row">
          {product.code}
        </StyledTableCell>
        <StyledTableCell>{product.name}</StyledTableCell>
        <StyledTableCell>{product.category}</StyledTableCell>
        <StyledTableCell>{product.saleQty}</StyledTableCell>
        <StyledTableCell>{product.combinedQty}</StyledTableCell>
        <StyledTableCell>
          
          <a className="edit-icon" onClick={handleEditItemBtn}>
            <svg
              width="16"
              height="16"
              viewBox="0 0 16 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M13.8125 4.6875L12.5938 5.90625L10.0938 3.40625L11.3125 2.1875C11.4375 2.0625 11.5938 2 11.7812 2C11.9688 2 12.125 2.0625 12.25 2.1875L13.8125 3.75C13.9375 3.875 14 4.03125 14 4.21875C14 4.40625 13.9375 4.5625 13.8125 4.6875ZM2 11.5L9.375 4.125L11.875 6.625L4.5 14H2V11.5Z"
                fill="#ED6C02"
              />
            </svg>
          </a>
          <a onClick={handlePopup}  className="deleteIcon">
            <svg
              width="16"
              height="16"
              viewBox="0 0 16 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M10.3438 2.65625H12.6562V4H3.34375V2.65625H5.65625L6.34375 2H9.65625L10.3438 2.65625ZM5.34375 6V12.6562H10.6562V6H5.34375ZM4 12.6562V4.65625H12V12.6562C12 13.0104 11.8646 13.3229 11.5938 13.5938C11.3229 13.8646 11.0104 14 10.6562 14H5.34375C4.98958 14 4.67708 13.8646 4.40625 13.5938C4.13542 13.3229 4 13.0104 4 12.6562Z"
                fill="#ED1C25"
              />
            </svg>
          </a>
        </StyledTableCell>
      </StyledTableRow>

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
              <span>Are you sure you want to delete this item?</span>
              <div className="buttons-container">
                <button onClick={onDelete} className="btnYesNo">
                  YES
                </button>
                <button onClick={() => setAnchorEl(null)} className="btnYesNo">
                  NO
                </button>
              </div>
            </div>
        </ClickAwayListener>
      </Popper>
    </>
  );
};

export default TableRowComponent;
