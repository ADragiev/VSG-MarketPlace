import { loadCategories, loadProductById } from "../global/itemsService.js"
import { makeRequest, postImage } from "../global/makeRequest.js"

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
                    name="code"
                    placeholder="Code *"
                    required
                    value="${product.code}"
                  />

                  <input
                    class="inputField"
                    type="text"
                    id="item-name"
                    name="fullName"
                    placeholder="Name *"
                    required
                    value="${product.fullName}"
                  />

                  <textarea
                    class="inputField"
                    id="item-description"
                    name="description"
                    rows="4"
                    required
                    placeholder="Description"
                  >${product.description}</textarea
                  >

                  <select
                    class="inputField"
                    id="edit-item-category"
                    name="categoryId"
                    required
                  >
                   
                  </select>

                  <input
                    class="inputField"
                    type="number"
                    id="quantity-for-sale"
                    name="saleQty"
                    min="1"
                    required
                    placeholder="Qty For Sale"
                    value="${product.saleQty}"
                  />

                  <input
                  class="inputField"
                  type="number"
                  id="price"
                  name="price"
                  min="1"
                  required
                  placeholder="Sale Price"
                  value="${product.price}"
                />

                  <input
                    class="inputField"
                    type="number"
                    id="quantity-available"
                    name="combinedQty"
                    min="0"
                    required
                    placeholder="Qty"
                    value="${product.combinedQty}"
                  />
                </div>

                <div class="imgSection">
                <input name="image"  id="editfileUpload" type="file" style="display: none;">
                  <img
                    class="editCurrentImg"
                    src="${product.image ? product.image : `../../images/no_image-placeholder.png`}"
                    alt="noImgPlaceholder"
                  />
                  <div class="img-buttons">
                    <button id"#addCurrentImg" class="upload-button" type="button">Upload</button>
                    <button id="edit-remove-button" type="button">Remove</button>
                  </div>
                </div>
              </div>

              <button id="submitFormBtn" type="submit">Modify</button>
            </form>
    `
    async function categories() {
      const selectList = document.querySelector('#edit-item-category')
      const categories = await loadCategories()
      categories.forEach(c => {
        let option = document.createElement('option')
        option.value = c.categoryId
        option.textContent = c.categoryName
        selectList.appendChild(option)
      })
    }
    categories()



    const formElement = document.querySelector(".edit-item-form");
    formElement.onsubmit = async (e) => {
    e.preventDefault();
    let data = Object.fromEntries(new FormData(e.target));
    console.log(data);
    const requestOptions = {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
  };
    let response = await fetch(`https://localhost:7054/Product/Update/${id}`, requestOptions);
    if (data.image.name) {
      //  data.delete('image')
      let imageFormData = new FormData()
      imageFormData.append("image", data.image)
      await postImage(id, imageFormData)
    }
    location.reload()
    console.log(response);
  editModal.style.display = 'none'
};


function uploadPicture() {
  const addImagePreview = document.querySelector(".editCurrentImg");

  document.querySelectorAll(".upload-button").forEach(btn =>{
     btn.addEventListener("click", (e) => {
        e.preventDefault();
        const input = document.querySelector("#editfileUpload");
        input.addEventListener("change", () => {
          const file = input.files[0];
          const reader = new FileReader();
          reader.onload = function (event) {
            addImagePreview.src = event.target.result;

          };
          reader.readAsDataURL(file);
        });
        input.click();
      });
  })
}
uploadPicture()



function removePicture() {
  document.querySelector('#edit-remove-button').addEventListener('click', ()=>{
      const addImagePreview = document.querySelector(".editCurrentImg");
      addImagePreview.src = '../../images/no_image-placeholder.png'
  })
}
removePicture()
}

////TODO: DELETE ITEM/////