import { loadPendingOrders } from "../global/itemsService.js";
import { createPendingOrderRow } from "./createPendingOrderRow.js";

const createOrders = async () => {
    const main = document.querySelector(".infoDetails");
    const products = await loadPendingOrders();

    products.forEach(p => {
        const div = createPendingOrderRow(p)
        main.append(div)
    });
  };
createOrders()