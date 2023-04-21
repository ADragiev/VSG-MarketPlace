import { loadInventoryItems, loadProducts } from "../global/itemsService.js";
import {  addBtn, showDeletePopup } from "./deleteTooltip.js";
import { showModal } from "./showEditModal.js";
import { createRow } from "./createTableRow.js";

const createTable = async () => {
  const tBody = document.querySelector(".main-table tbody");
  const products = await loadInventoryItems();

  products.forEach((product) => {
    const row = createRow(product);
    tBody.appendChild(row);
    // showModal(product);
  });

  document.querySelector(".input-div input").addEventListener("input", (e) => {
    tBody.innerHTML = ``;
    const searchValue = e.target.value.toLowerCase();
    if (searchValue) {
      const filteredProducts = products.filter((p) =>
        p.fullName.toLowerCase().includes(searchValue)
      );
      filteredProducts.forEach((product) => {
        const row = createRow(product);
        tBody.appendChild(row);
      });
    } else {
      products.forEach((product) => {
        const row = createRow(product);
        tBody.appendChild(row);
      });
    }
  });

  

};

createTable();
