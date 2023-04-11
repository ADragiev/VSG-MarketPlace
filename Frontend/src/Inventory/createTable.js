import { loadProducts } from "../global/itemsService.js";
import { showDeletePopup, addBtn } from "./deleteTooltip.js";
import { showEditForm } from "./addNewItemModal.js";
import { showModal } from "./showEditModal.js";
import { createRow } from "./createTableRow.js";

const createTable = async () => {
  const tBody = document.querySelector(".main-table tbody");
  const products = await loadProducts();

  document.querySelector(".input-div input").addEventListener("input", (e) => {
    tBody.innerHTML = ``;
    const searchValue = e.target.value.toLowerCase();
    if (searchValue) {
      const filteredProducts = products.filter((p) =>
        p.title.toLowerCase().includes(searchValue)
      );
      filteredProducts.forEach((product) => {
        const row = createRow(product);
        tBody.appendChild(row);
        showDeletePopup();
        addBtn();
        showEditForm();
        showModal();
      });
    } else {
      products.forEach((product) => {
        const row = createRow(product);
        showDeletePopup();
        addBtn();
        showEditForm();
        showModal();

        tBody.appendChild(row);
      });
    }
  });

  products.forEach((product) => {
    const row = createRow(product);
    tBody.appendChild(row);
  });

  showDeletePopup();
  addBtn();
  showEditForm();
  showModal();
};

createTable();
