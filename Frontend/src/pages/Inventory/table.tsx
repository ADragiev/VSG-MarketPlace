import { styled } from "@mui/material/styles";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { useGetInventoryProductsQuery } from "../../services/productService";
import { TableFooter, TablePagination } from "@mui/material";
import { IInventoryItem } from "../../types";
import { useEffect, useState } from "react";
import TableRowComponent from "./TableRow";

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    fontWeight: 600,
    border: 1,
    fontSize: 17,
    backgroundColor: theme.palette.common.white,
    color: theme.palette.common.black,
  },
  [`&.${tableCellClasses.body}`]: {
    fontWeight: 500,
    fontSize: 16,
  },
}));

type TableProps = {
  searchQuery: string;
  locationValue: number,
  products: IInventoryItem[],
  setProducts: React.Dispatch<React.SetStateAction<IInventoryItem[]>>
  
};

export default function CustomizedTables({ searchQuery, locationValue, products, setProducts }: TableProps) {
  const rowsPerPage = 10;
  const [page, setPage] = useState(0);
  const { data } = useGetInventoryProductsQuery("");

  let filteredPRoducts = []

  useEffect(() => {
    if (data) {
      setProducts(data);
    }
  }, [data]);

  const handleOnPageChange = (e: React.MouseEvent<HTMLButtonElement, MouseEvent> | null, newPage: number) =>{
    console.log(e);  
    setPage(newPage)
  }


  if (locationValue === 0) {
     filteredPRoducts = products.filter((p) =>
    p.name.toLowerCase().includes(searchQuery)
  );
  }
  else{
    filteredPRoducts = products.filter((p) =>
   p.locationId == locationValue
  ).filter((p) =>
  p.name.toLowerCase().includes(searchQuery)
);
  }
 

  return (
    <TableContainer className="tableContainer">
      <Table aria-label="customized table">
        <TableHead className="tableHead">
          <TableRow>
            <StyledTableCell>Code</StyledTableCell>
            <StyledTableCell>Name</StyledTableCell>
            <StyledTableCell>Category</StyledTableCell>
            <StyledTableCell>For Sale</StyledTableCell>
            <StyledTableCell>QTY</StyledTableCell>
            <StyledTableCell>Actions</StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {filteredPRoducts
            .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
            .map((row) => (
              <TableRowComponent products={products} setProducts={setProducts} product={row} key={row.id} />
            ))}
            {filteredPRoducts.length === 0 && <div className="noProductsDiv">No products found</div>}
        </TableBody>
        <TableFooter>
          <TableRow>
            <TablePagination
              rowsPerPageOptions={[10]}
              count={filteredPRoducts.length}
              rowsPerPage={rowsPerPage}
              page={page}
              onPageChange={handleOnPageChange}
            />
          </TableRow>
        </TableFooter>
      </Table>
    </TableContainer>
  );
}
