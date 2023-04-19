import { createOrderRow } from "./createMyOrderRow.js";

const createMyOrders = async () => {
    const main = document.querySelector(".infoDetails");
    const products = await fetch('https://localhost:7054/Order/MyOrders?email=user');
    const data = await products.json();
    data.forEach((p) => {
      const div = createOrderRow(p);
      let completeBtn = div.querySelector(".deleteIcon");
      completeBtn.addEventListener("click", async () => {

        
        // await fetch(`https://localhost:7054/Order/Reject/${div.id}`, {
        //   method: "DELETE",
        // });
      });
      main.append(div);
    });
  };
  createMyOrders();