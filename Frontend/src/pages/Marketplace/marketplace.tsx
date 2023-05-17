import {  useGetProductsQuery } from "../../services/productService";
import { IProduct } from "../../types";
import Card from "../../components/Product/product";

const MarketPlace = (): JSX.Element => {
  const {data: products} = useGetProductsQuery('fasdfsa')

  return (
    <>
    <main className="main" id="main-list-wrapper">
      {products?.map((product: IProduct) => (
          <Card product={product} key= {product.id} />
          ))}
          </main>
    </>
  );
};

export default MarketPlace;
