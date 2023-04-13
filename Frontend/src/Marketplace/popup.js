import { makeRequest } from "../global/makeRequest.js";

 export function showPopup() {
  const buyBtn = Array.from(document.getElementsByClassName("circle"));
  buyBtn.forEach((x) =>
    x.addEventListener("click", (e) => {
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

        let qty = document.querySelector('.selectQty').value
        let orderedBy = 'user'
        let productId = e.target.parentElement.parentElement.parentElement.parentElement.parentElement.id;
        if (e.target.textContent == "YES") {
          let data = {
            qty,
            orderedBy,
            productId
          };
          let response = await makeRequest({
            path: "/Order",
            method: "POST",
            data
            
          });
          console.log(response);
          // location.reload()
        }
       
    });
  });
}

