import { useGetProductsQuery } from "../../services/productService";
import { IProduct } from "../../types";
import Card from "../../components/Product/Product";
import { CircularProgress } from "@mui/material";
import SearchOffIcon from '@mui/icons-material/SearchOff';

const MarketPlace = (): JSX.Element => {
  const { data: products, isLoading } = useGetProductsQuery("");

  return (
    <>
     <main className="main" id="main-list-wrapper" role="main">
        {isLoading ? (
          <CircularProgress className="marketplace-loader" />
        ) : (
          <>
            {products && products.length > 0 ? (
              products.map((product: IProduct) => (
                <Card product={product} key={product.id} />
              ))
            ) : (
              <div className="no-products-message">
              <SearchOffIcon className="no-products-icon"/>
              <div >No products available</div>
              </div>
            )}
          </>
        )}
      </main>
    </>
  );
};

export default MarketPlace;
