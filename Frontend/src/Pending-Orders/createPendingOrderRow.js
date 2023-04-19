export function createPendingOrderRow(product){
    const div = document.createElement("div");
      div.className = 'item-row'
          div.id = product.id
          div.innerHTML = `
         
          <div class="div-wrapper">
            <span class="codeColumn">${product.code}</span>
            <span class="qtyColumn">${product.qty}</span>
            <span class="priceColumn">${product.price} BGN</span>
          </div>
          <span class="emailColumn">${product.orderedBy}</span>
          <span class="dateColumn">${product.orderDate}</span>
          <button class="btnColumn completeBtn" >Complete</button>
       
      `

      
      return div;
}