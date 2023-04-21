import { loadCategories, loadLocations, postImageById } from "../global/itemsService.js";
import { makeRequest, postImage } from "../global/makeRequest.js";

const modal = document.querySelector(".add-item-modal");
const editModal = document.querySelector(".edit-item-modal");

document.getElementById("addNewItemBtn").addEventListener("click", () => {
  modal.style.display = "block";
});

 async function categories() {
  const selectList = document.querySelector('#item-category')
  const categories = await loadCategories()
  categories.forEach(c => {
    let option = document.createElement('option')
    option.value = c.id
    option.textContent = c.name
    selectList.appendChild(option)
  })
}
categories()

async function locations() {
  const locationList = document.querySelector('#location')
  const locations = await loadLocations()
  locations.forEach(l => {
    let option = document.createElement('option')
    option.value = l.id
    option.textContent = l.name
    locationList.appendChild(option)
  })
}
locations()

const formElement = document.querySelector(".add-item-form");
formElement.onsubmit = async (e) => {
  e.preventDefault();
  let formData = new FormData(e.target)
  let image = formData.get('image')
  
  // console.log(Array.from(newFormData));
  // console.log(Array.from(formData));
  // ^^ WORKS 
  let data = Object.fromEntries(formData);
  let response = await makeRequest({
    path: "/Product",
    method: "POST",
    data
  });
  let productId = response.id
  // productId = 20...
  if (image.name) {
    let imageFormData = new FormData()
    imageFormData.append("image", image)
    formData.delete('image')
    await postImage(productId, imageFormData)
  }
  location.reload()
  modal.style.display = 'none'
};



  const closeBtn = modal.querySelector(".close-modal-button");
  closeBtn.addEventListener("click", () => {
      modal.style.display = "none";
    });

 function uploadPicture() {
  const addImagePreview = document.querySelector("#addCurrentImg");

  document.querySelectorAll(".upload-button").forEach(btn =>{
     btn.addEventListener("click", (e) => {
        e.preventDefault();
        const input = document.querySelector("#fileUpload");
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

  document.querySelector('#remove-button').addEventListener('click', ()=>{
    let input = document.querySelector('#fileUpload')
    input.value = ''
    const addImagePreview = document.querySelector("#addCurrentImg");
    addImagePreview.src = '../../images/no_image-placeholder.png'
  })
}
removePicture()
  
