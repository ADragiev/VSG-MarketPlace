
import { addBtn, showPopup } from "./popup.js";

export const createCard = (id, image, category, price) => {
  const listWrapper = document.querySelector(".main");

  const cardDiv = document.createElement("div");
  cardDiv.id = id;
  cardDiv.className = "card-item";
  cardDiv.innerHTML = `
    <a class="product-image">
    <img
    
    src="${image}"
    alt="ProductImage"
  />
    </a>
  <div class="details">
    <div class="name-price">
      <p>${price} BGN</p>
      <p>${category}</p>
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
          <strong>${price} BGN?</strong>
        </span>
        <div class="buttons-container">
          <button class="btnYesNo">YES</button>
          <button class="btnYesNo">NO</button>
        </div>
      </div>
      <a class="circle" id="firstBtn">
        <img src="../images/dollar.svg" alt="DollarImage" />
      </a>
    </div>
  </div>
    `;

  const select = cardDiv.querySelector(".selectQty");
  const randomNum = Math.floor(Math.random() * 11 + 1);

  for (let i = 1; i < randomNum + 1; i++) {
    const option = document.createElement("option");
    option.value = i;
    option.text = i;
    if (i == 1) {
      option.selected = true;
    }

    select.appendChild(option);
  }
  listWrapper.appendChild(cardDiv);

  showPopup();
  addBtn();
};
