import { loadProducts } from "../src/itemsService.js"
import { showDeletePopup, addBtn } from "./deleteTooltip.js"
import { showEditForm } from "./addNewItemModal.js"
import { showModal } from "./showEditModal.js"

const createTableRow = async () =>{

    const tBody = document.querySelector('.main-table tbody')
    const products = await loadProducts()

    products.forEach(product => {
        const row = document.createElement("tr");
        row.id = product.id
        row.innerHTML = `
    <td>${product.id}</td>
    <td>${product.title}</td>
    <td>${product.category}</td>
    <td>0</td>
    <td>2</td>
    <td class="actionButtons">
      <a class="edit-icon"
        ><svg
          width="16"
          height="16"
          viewBox="0 0 16 16"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
          class="editIcon"
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
    `
    tBody.appendChild(row)
    })

    showDeletePopup()
    addBtn()
    showEditForm()
    showModal()

}

createTableRow()