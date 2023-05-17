import { useRef, useState } from "react";
import { IProduct } from "../../types";
import ProductModal from "./productModal";



import { createOrder } from "../../services/itemsServices";
import PopperComponent from "../Popper";

type ProductProps = {
  product: IProduct;
};
const Card = ({ product }: ProductProps): JSX.Element => {
  const [anchorEl, setAnchorEl] = useState(null);

  const options = [];
  for (let i = 1; i <= product.saleQty; i++) {
    options.push({ value: i });
  }

  const handlePopup = (e) => {
    setAnchorEl(e.currentTarget);
  };

  const [isModalOpen, setIsModalOpen] = useState(false);
  const selectValue = useRef(1);

  const onBuy = async () => {
    await createOrder(product.id, selectValue.current);
    setAnchorEl(null)
  };

  const handleOnImageClick = () => {
    setIsModalOpen(true);
  };

  const handleSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    selectValue.current = Number(event.target.value);
  };


  const string = `Are you sure you want to buy 
  ${selectValue.current} item for   
    ${Number(selectValue.current) * product.price}
  ?`

  return (
    <>
      {isModalOpen && (
        <ProductModal product={product} onClose={() => setIsModalOpen(false)} />
      )}
      <div className="card-item">
        <a className="product-image" onClick={handleOnImageClick}>
          <img
            src={
              product.image
                ? product.image
                : `./images/no_image-placeholder.png`
            }
            alt="ProductImage"
          />
        </a>
        <div className="details">
          <div className="name-price">
            <p>{product.price} BGN</p>
            <p>{product.category}</p>
          </div>
          <div className="details-wrapper">
            <div className="qty">
              <p>Qty</p>
              <select
                name="qty"
                className="selectQty"
                onChange={handleSelectChange}
              >
                {options.map((o) => (
                  <option value={o.value} key={o.value}>
                    {o.value}
                  </option>
                ))}
              </select>
            </div>

            <div className="icon popup">
              <a className="circle" id="firstBtn" onClick={handlePopup}>
                <img src="../../images/dollar.svg" alt="DollarImage" />
              </a>
            </div>
          </div>
        </div>
      </div>
      <PopperComponent str={string} onYes={onBuy} anchor={anchorEl} setAnchor={setAnchorEl}/>
    </>
  );
};

export default Card;