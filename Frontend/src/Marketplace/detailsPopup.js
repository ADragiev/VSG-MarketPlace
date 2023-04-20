import { createModal } from "./createModal.js";

const modal = document.querySelector(".modal");
export async function showModal(cardDiv, product) {
  const productImage = cardDiv.querySelector(".product-image");
   productImage.addEventListener("click", async (e) => {
      e.preventDefault();
      await createModal(product);
      modal.style.display = "flex";
      closeModal()
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
