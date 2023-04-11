import '../global/header.js';
import '../global/hamburger.js';
import '../global/itemsService.js';
import '../global/makeRequest.js';
import './createModal.js';


import { createCard } from "./card.js";
import { showModal } from "./detailsPopup.js";
import { loadProducts } from "../global/itemsService.js"

const productsData = await loadProducts();
productsData.forEach((product) => {
  createCard(product.id, product.image, product.category, product.price);
});
showModal();
