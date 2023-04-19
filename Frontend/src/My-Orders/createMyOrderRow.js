import { confirmBtn, showConfirmRejectPopup } from "./confirmRejectPopup.js";

export function createOrderRow(product){
    const div = document.createElement("div");
      div.className = 'item-row extend'
          div.id = product.id
          div.innerHTML = `
          <span class="ProductNameColumn"
            >${product.productName}</span
          >
          <span class="ProductQtyColumn">${product.qty}</span>
          <span class="ProductPriceColumn">${product.price} BGN</span>
          <span class="ProductDateColumn">${product.orderDate}</span>
          <div class="ProductStatus popup">
            <span class="status">${product.orderStatus}</span>
            <div class="popuptext">
              <span> Are you sure you want to reject this order? </span>
              <div class="buttons-container">
                <button class="btnYesNo">YES</button>
                <button class="btnYesNo">NO</button>
              </div>
            </div>
            <a class="deleteIcon" style="display:${product.orderStatus=='Finished' ? 'none' : 'block'}">
              <svg
              
              width="12"
              height="12"
              viewBox="0 0 12 12"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M11.8203 1.35156L7.17188 6L11.8203 10.6484L10.6484 11.8203L6 7.17188L1.35156 11.8203L0.179688 10.6484L4.82812 6L0.179688 1.35156L1.35156 0.179688L6 4.82812L10.6484 0.179688L11.8203 1.35156Z"
                fill="#ED1C25"
              />
            </svg>
            </a>
           
          </div>
      `  

      confirmBtn(div)
      showConfirmRejectPopup(div)
      return div;
}