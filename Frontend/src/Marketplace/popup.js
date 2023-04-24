import { makeRequest } from "../global/makeRequest.js";

 export function showPopup(cardDiv, prize) {
  const buyBtn = Array.from(document.getElementsByClassName("circle"));
  buyBtn.forEach((x) =>
       x.addEventListener("click", (e) => {
     let span = cardDiv.querySelector('.popuptext span')
     let qty = cardDiv.querySelector('.selectQty').value
      span.innerHTML = `Are you sure you want to buy <strong>${qty}</strong> ${qty == 1 ? 'item' : 'items'} for <strong>${prize*qty}</strong>?`
      e.target.parentElement.parentElement
        .querySelector(".popuptext")
        .classList.add("show");
    })
  );
}

export function addBtn(div) {
  div.querySelectorAll(".btnYesNo").forEach((x) => {
    x.addEventListener("click", async (e) => {
        e.preventDefault()
        e.target.parentElement.parentElement.className = 'popuptext'

        let detailsDiv =  e.target.parentElement.parentElement.parentElement.parentElement
        let qty = detailsDiv.querySelector('.selectQty').value
        let orderedBy = 'user'
        let productId = e.target.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.id;
        if (e.target.textContent == "YES") {
          let data = {
            qty,
            orderedBy,
            productId
          };
         await makeRequest({
            path: "/Order",
            method: "POST",
            data
          });
          location.reload()
        }
       
    });
  });
}

