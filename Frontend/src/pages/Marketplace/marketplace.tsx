import { useEffect, useState } from "react";
import { loadProducts } from "../../services/itemsServices";
import { IProduct } from "../../types";
import Card from "../../components/Product/product";

const MarketPlace = (): JSX.Element => {
  const [products, setProduct] = useState<IProduct[]>([]);

 



  useEffect(() => {
    const resultFunc = async () => {
      const result: IProduct[] = await loadProducts();
      setProduct(result);
    };
    resultFunc();
  }, []);
  return (
    <>
    <main className="main" id="main-list-wrapper">
      {products.map((product) => (
          <Card product={product} key= {product.id} />
          ))}
          </main>
    </>
  );
};

export default MarketPlace;
