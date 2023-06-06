import { useGetProductsQuery } from "../../services/productService";
import { IProduct } from "../../types";
import Card from "../../components/Product/Product";
import { CircularProgress } from "@mui/material";

const MarketPlace = (): JSX.Element => {
  const { data: products, isLoading } = useGetProductsQuery("");

  return (
    <>
      <main className="main" id="main-list-wrapper" role="main">
        {isLoading ? (
          <CircularProgress className="marketplace-loader" />
        ) : (
          products?.map((product: IProduct) => (
            <Card product={product} key={product.id} />
          ))
        )}
      </main>
    </>
  );
};

export default MarketPlace;
