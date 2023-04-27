import { createEditModal } from "./createEditModal.js";

const modal = document.querySelector(".edit-item-modal");
export async function showModal(product) {
  const editIcons = document.querySelectorAll(".edit-icon");
  editIcons.forEach((editIcon) => {
    editIcon.addEventListener("click", async (e) => {
      e.preventDefault();
      await createEditModal(product);
      modal.style.display = "flex";
      closeModal();
    });
  });
}
export function closeModal(modal) {
  modal.querySelector(".close-modal-button").addEventListener("click", closing);
}

function closing(e) {
  e?.preventDefault();
  e.target.parentElement.remove();
  modal.style.display = "none";
}
