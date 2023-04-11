import { createModal } from "./createModal.js";

const modal = document.querySelector(".modal");
export async function showModal() {
  const productDivs = document.querySelectorAll(".product-image");
  productDivs.forEach((productDiv) => {
    productDiv.addEventListener("click", async (e) => {
      e.preventDefault();
      const productId = e.target.parentElement.parentElement.id;
      await createModal(productId);
      modal.style.display = "flex";
      closeModal()
    });
  });
}
export function closeModal() {
  document
    .querySelectorAll(".close-button")
    .forEach((b) => b.addEventListener("click", closing));
}

function closing(e) {
  e?.preventDefault();
    e.target.parentElement.remove()
    modal.style.display = "none";
}
