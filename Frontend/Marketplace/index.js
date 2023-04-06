import { createCard } from "./card.js";
import { showModal } from "./detailsPopup.js";
import { loadProducts } from "../src/itemsService.js"

const productsData = await loadProducts();
productsData.forEach((product) => {
  createCard(product.id, product.image, product.category, product.price);
});
showModal();
