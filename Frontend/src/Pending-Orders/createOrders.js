import { loadPendingOrders } from "../global/itemsService.js";
import { createPendingOrderRow } from "./createPendingOrderRow.js";

const createOrders = async () => {
  const main = document.querySelector(".infoDetails");
  const products = await loadPendingOrders();

  products.forEach((p) => {
    const div = createPendingOrderRow(p);
    let completeBtn = div.querySelector(".completeBtn");
    completeBtn.addEventListener("click", async () => {
      await fetch(`https://localhost:7054/Order/${div.id}`, {
        method: "PUT",
      });
      location.reload();
    });
    main.append(div);
  });
};
createOrders();
