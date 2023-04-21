import { createEditModal } from "./createEditModal";
import { closeModal, showModal } from "./showEditModal";

export function createRow(product) {
  const row = document.createElement("tr");
  row.id = product.id;
  row.innerHTML = `
      <td>${product.code}</td>
      <td>${product.name}</td>
      <td>${product.category}</td>
      <td>${product.saleQty}</td>
      <td>${product.combinedQty}</td>
      <td class="actionButtons">
        <a class="edit-icon"
          ><svg
            width="16"
            height="16"
            viewBox="0 0 16 16"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
            
          >
            <path
              d="M13.8125 4.6875L12.5938 5.90625L10.0938 3.40625L11.3125 2.1875C11.4375 2.0625 11.5938 2 11.7812 2C11.9688 2 12.125 2.0625 12.25 2.1875L13.8125 3.75C13.9375 3.875 14 4.03125 14 4.21875C14 4.40625 13.9375 4.5625 13.8125 4.6875ZM2 11.5L9.375 4.125L11.875 6.625L4.5 14H2V11.5Z"
              fill="#ED6C02"
            /></svg>
      </a>
        <div class="popup">
          <div class="popuptext">
            <span> Are you sure you want to delete this item? </span>
            <div class="buttons-container">
              <button class="btnYesNo">YES</button>
              <button class="btnYesNo">NO</button>
            </div>
          </div>
          <a class="deleteIcon">
            <svg
              width="16"
              height="16"
              viewBox="0 0 16 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M10.3438 2.65625H12.6562V4H3.34375V2.65625H5.65625L6.34375 2H9.65625L10.3438 2.65625ZM5.34375 6V12.6562H10.6562V6H5.34375ZM4 12.6562V4.65625H12V12.6562C12 13.0104 11.8646 13.3229 11.5938 13.5938C11.3229 13.8646 11.0104 14 10.6562 14H5.34375C4.98958 14 4.67708 13.8646 4.40625 13.5938C4.13542 13.3229 4 13.0104 4 12.6562Z"
                fill="#ED1C25"
              />
            </svg>
          </a>
        </div>
      </td>
      `;

  const deleteBtn = row.querySelector(".deleteIcon");
  deleteBtn.addEventListener("click", (e) => {
    e.target.parentElement.parentElement.parentElement
      .querySelector(".popuptext")
      .classList.add("show");
  });

  row.querySelectorAll(".btnYesNo").forEach(btn =>{
    btn.addEventListener("click", async (e) => {
      e.preventDefault();
      e.target.parentElement.parentElement.className = "popuptext";
      if (e.target.textContent == "YES") {
        let response = await fetch(
          `https://localhost:7054/Product/${product.id}`,
          {
            method: "DELETE",
          }
        );
        location.reload();
        console.log(response);
      }
      else{
      e.target.parentElement.parentElement.className = "popuptext";
      }
  })
  });
  
  let modal = document.querySelector('.edit-item-modal')


  
  const editIcons = row.querySelector(".edit-icon");
  editIcons.addEventListener("click", async () => {
    modal.style.display = "flex";
    await createEditModal(product);
    closeModal(modal);
      });
  
    const closeBtn = document.querySelector(".close-modal-button");
    closeBtn.addEventListener("click", () => {
        modal.style.display = "none";
      });


  return row;
}
