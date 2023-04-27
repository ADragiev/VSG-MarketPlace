import "../global/header.js";
import "../global/hamburger.js";
import "../global/itemsService.js";
import "../global/makeRequest.js";
import "./createModal.js";

import { createCard } from "./card.js";
import { loadProducts } from "../global/itemsService.js";

const main = document.querySelector("#main-list-wrapper");

const productsData = await loadProducts();
if (productsData.length > 0) {
  productsData.forEach((product) => {
    createCard(product);
  });
} else {
  main.innerHTML = `<span id="no-items-span">No items for sale</span>`;
}
