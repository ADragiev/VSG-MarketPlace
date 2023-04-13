import { createEditModal } from "./createEditModal.js";

const modal = document.querySelector(".edit-item-modal");
export async function showModal() {
  const editIcons = document.querySelectorAll(".edit-icon");
  editIcons.forEach((editIcon) => {
    editIcon.addEventListener("click", async (e) => {
      e.preventDefault();

      const rowId = e.target.parentElement.parentElement.id;
      await createEditModal(rowId);
      modal.style.display = "flex";
      closeModal();
    });
  });
}
export function closeModal() {
  document
    .querySelectorAll(".close-modal-button")
    .forEach((b) => b.addEventListener("click", closing));
}

function closing(e) {
  e?.preventDefault();
  e.target.parentElement.remove();
  modal.style.display = "none";
}
