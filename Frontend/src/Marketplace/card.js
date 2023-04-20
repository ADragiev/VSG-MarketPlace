
import { showModal } from "./detailsPopup.js";
import { addBtn, showPopup } from "./popup.js";

export const createCard = (product) => {
  const listWrapper = document.querySelector(".main");
  let priceToPass = product.price
  const cardDiv = document.createElement("div");
  cardDiv.id = product.id;
  cardDiv.className = "card-item";
  cardDiv.innerHTML = `
    <a class="product-image">
    <img
    
    src="${product.image ? product.image : `../../images/no_image-placeholder.png` }"
    alt="ProductImage"
  />
    </a>
  <div class="details">
    <div class="name-price">
      <p>${product.price} BGN</p>
      <p>${product.category}</p>
    </div>
    <div class="qty">
      <p>Qty</p>
      <select name="qty" class="selectQty">
       
      </select>
    </div>

    <div class="icon popup">
      <div class="popuptext">
        <span>
          Are you sure you want to buy <strong>1</strong> item for
          <strong>${product.price} BGN?</strong>
        </span>
        <div class="buttons-container">
          <button class="btnYesNo">YES</button>
          <button class="btnYesNo">NO</button>
        </div>
      </div>
      <a class="circle" id="firstBtn">
        <img src="../../images/dollar.svg" alt="DollarImage" />
      </a>
    </div>
  </div>
    `;

  const select = cardDiv.querySelector(".selectQty");
  

  for (let i = 1; i < product.saleQty + 1; i++) {
    const option = document.createElement("option");
    option.value = i;
    option.text = i;
    select.appendChild(option);
  }
  listWrapper.appendChild(cardDiv);
  
  addBtn(cardDiv);
  showPopup(cardDiv, priceToPass);
  showModal(cardDiv, product);

};
