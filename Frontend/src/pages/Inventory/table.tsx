import { useGetInventoryProductsQuery } from "../../services/productService";
import { IInventoryItem } from "../../types";
import { useEffect } from "react";
import TableRowComponent from "./TableRow";
import { DataGrid, GridRowsProp, GridColDef } from "@mui/x-data-grid";
import { CircularProgress } from "@mui/material";

type TableProps = {
  searchQuery: string;
  products: IInventoryItem[];
  setProducts: React.Dispatch<React.SetStateAction<IInventoryItem[]>>;
};

export default function CustomizedTables({
  searchQuery,
  products,
  setProducts,
}: TableProps) {
  const { data, isLoading } = useGetInventoryProductsQuery();
  let filteredPRoducts = []

  filteredPRoducts = products.filter((p) =>
  p.name.toLowerCase().includes(searchQuery))

  useEffect(() => {
    if (data) {
      setProducts(data);
    }
  }, [data]);

  const rows: GridRowsProp = filteredPRoducts.map((item) => ({
    id: item.id,
    code: item.code,
    name: item.name,
    category: item.category,
    forSale: item.saleQty,
    qty: item.combinedQty,
    lendQty: item.lendQty,
    location: item.location,
    actions: item,
  }));
  const columns: GridColDef[] = [
    { field: "code", headerName: "Code", flex: 1 },
    { field: "name", headerName: "Name", flex: 1 },
    { field: "category", headerName: "Category", flex: 1 },
    { field: "forSale", headerName: "For sale", flex: 1 },
    { field: "qty", headerName: "QTY", flex: 1 },
    { field: "lendQty", headerName: "Lend QTY", flex: 1 },
    { field: "location", headerName: "Location", flex: 1 },
    {
      field: "actions",
      headerName: "Action",
      flex: 1,
      renderCell: (params) => {
        const product = params.value as IInventoryItem;
        return (
          <TableRowComponent setProducts={setProducts} product={product} />
        );
      },
    },
  ];

  return (
    <div style={{ height: "70vh", width: "100%" }}>

      {isLoading ? <CircularProgress className="spinning-loader" /> : 
      <DataGrid
        rows={rows}
        columns={columns}
        initialState={{
          pagination: {
            paginationModel: { pageSize: 10, page: 0 },
          },
        }}
      />
}
    </div>
  );
}
