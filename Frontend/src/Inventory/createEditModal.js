import { loadProductById } from "../global/itemsService.js"
import { makeRequest } from "../global/makeRequest.js"

export const createEditModal = async (id) =>{

    const editModal = document.querySelector('.edit-item-modal')
    const product = await loadProductById(id)

    editModal.innerHTML = `
    <form class="edit-item-form">
              <a class="close-modal-button">
                <svg
                  width="18"
                  height="18"
                  viewBox="0 0 18 18"
                  fill="none"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path
                    d="M17.7305 2.02734L10.7578 9L17.7305 15.9727L15.9727 17.7305L9 10.7578L2.02734 17.7305L0.269531 15.9727L7.24219 9L0.269531 2.02734L2.02734 0.269531L9 7.24219L15.9727 0.269531L17.7305 2.02734Z"
                    fill="black"
                  />
                </svg>
              </a>
              <div class="row">
                <div class="left-side">
                  <span>Modify Item</span>
                  <input
                    class="inputField"
                    type="text"
                    id="item-code"
                    name="itemCode"
                    placeholder="Code *"
                    required
                    value="${product.id}"
                  />

                  <input
                    class="inputField"
                    type="text"
                    id="item-name"
                    name="itemName"
                    placeholder="Name *"
                    required
                    value="${product.title}"
                  />

                  <textarea
                    class="inputField"
                    id="item-description"
                    name="itemDescription"
                    rows="4"
                    required
                    placeholder="Description"
                  >${product.description}</textarea
                  >

                  <select
                    class="inputField"
                    id="item-category"
                    name="itemCategory"
                    required
                  >
                    <option value="" disabled selected>Category *</option>
                    <option value="electronics">Electronics</option>
                    <option value="fashion">Fashion</option>
                    <option value="home">Home</option>
                    <option value="sports">Sports</option>
                  </select>

                  <input
                    class="inputField"
                    type="number"
                    id="quantity-for-sale"
                    name="quantityForSale"
                    min="1"
                    required
                    placeholder="Qty For Sale"
                    value="0"
                  />

                  <input
                  class="inputField"
                  type="number"
                  id="sale-price"
                  name="salePrice"
                  min="1"
                  required
                  placeholder="Sale Price"
                  value="${product.price}"
                />

                  <input
                    class="inputField"
                    type="number"
                    id="quantity-available"
                    name="quantityAvailable"
                    min="0"
                    required
                    placeholder="Qty"
                    value="2"
                  />
                </div>

                <div class="imgSection">
                  <img
                    class="editCurrentImg"
                    src="${product.image}"
                    alt="noImgPlaceholder"
                  />
                  <div class="img-buttons">
                    <button class="upload-button" type="button">Upload</button>
                    <button id="remove-button" type="button">Remove</button>
                  </div>
                </div>
              </div>

              <button id="submitFormBtn" type="submit">Add</button>
            </form>
    `

    const formElement = document.querySelector(".edit-item-form");
    formElement.onsubmit = async (e) => {
    e.preventDefault();
    let formData = Object.fromEntries(new FormData(e.target));
    let response = await makeRequest({
        path: "/products/" + id,
        method: "PUT",
        data: formData,
    });
  editModal.style.display = 'none'
  console.log(response);
};
}

////TODO: DELETE ITEM/////