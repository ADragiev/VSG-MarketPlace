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
import TableRowComponent from "./tableRow";

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
};

export default function CustomizedTables({ searchQuery }: TableProps) {
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const { data } = useGetInventoryProductsQuery("");

  const [products, setProducts] = useState<IInventoryItem[]>([]);

  useEffect(() => {
    if (data) {
      setProducts(data);
    }
  }, [data]);

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  const filteredPRoducts = products.filter((p) =>
    p.name.toLowerCase().includes(searchQuery)
  );

  return (
    <TableContainer sx={{ border: "1px solid #B3B3B3", borderRadius: "4px" }}>
      <Table aria-label="customized table">
        <TableHead sx={{ borderBottom: "1px solid #B3B3B3" }}>
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
              <TableRowComponent product={row} key={row.id} />
            ))}
        </TableBody>
        <TableFooter>
          <TableRow>
            <TablePagination
              rowsPerPageOptions={[10]}
              count={filteredPRoducts.length}
              rowsPerPage={rowsPerPage}
              page={page}
              onPageChange={(e, newPage) => setPage(newPage)}
              onRowsPerPageChange={handleChangeRowsPerPage}
            />
          </TableRow>
        </TableFooter>
      </Table>
    </TableContainer>
  );
}
