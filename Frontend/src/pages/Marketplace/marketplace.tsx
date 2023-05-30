import { useGetProductsQuery } from "../../services/productService";
import { IProduct } from "../../types";
import Card from "../../components/Product/Product";

const MarketPlace = (): JSX.Element => {
  const { data: products, isLoading } = useGetProductsQuery("");

  return (
    <>
      <main className="main" id="main-list-wrapper" role='main'>
        {products?.map((product: IProduct) => (
          <Card product={product} key={product.id} />
        ))}
        {isLoading && <div>Loading...</div>}
      </main>
    </>
  );
};

export default MarketPlace;
