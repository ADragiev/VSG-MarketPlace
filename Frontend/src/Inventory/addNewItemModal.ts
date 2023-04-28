import { loadCategories, loadLocations } from "../global/itemsService.js";
import { makeRequest, postImage } from "../global/makeRequest.js";

const modal = document.querySelector(".add-item-modal") as HTMLElement;

(document.getElementById("addNewItemBtn") as HTMLElement).addEventListener("click", () => {
  modal.style.display = "block";
  modal.addEventListener('click', (e)=>{
    const target = e.target as HTMLElement;
    if (target.className == 'add-item-modal') {
  modal.style.display = "none";  
    }
  },)
});

 async function categories() {
  const selectList = document.querySelector('#item-category') as HTMLElement
  const categories = await loadCategories()
  categories.forEach((c: any) => {
    let option = document.createElement('option') as HTMLOptionElement
    option.value = c.id
    option.textContent = c.name
    selectList.appendChild(option)
  })
}
categories()

async function locations() {
  const locationList = document.querySelector('#location') as HTMLElement
  const locations = await loadLocations()
  locations.forEach((l: any) => {
    let option = document.createElement('option')
    option.value = l.id
    option.textContent = l.name
    locationList.appendChild(option)
  })
}
locations()

const formElement = document.querySelector(".add-item-form") as HTMLElement;
formElement.onsubmit = async (e) => {
  e.preventDefault();
  const target = e.target as HTMLFormElement
  let formData = new FormData(target)
  let image = formData.get('image')  as File
  
  
  let data = Object.fromEntries(formData) ;
  let response = await makeRequest({
    path: "/Product",
    method: "POST",
    data
  });
  let productId = response.id
  if (image.name) {
    let imageFormData = new FormData()
    imageFormData.append("image", image)
    formData.delete('image')
    await postImage(productId, imageFormData)
  }
  modal.style.display = 'none'
  location.reload()
};



  const closeBtn = modal.querySelector(".close-modal-button") as HTMLElement;
  closeBtn.addEventListener("click", () => {
      modal.style.display = "none";
    });

    const input = document.querySelector("#fileUpload") as HTMLInputElement;
    const addImagePreview = document.querySelector("#addCurrentImg") as HTMLImageElement;

 function uploadPicture() {

  document.querySelectorAll(".upload-button").forEach(btn =>{
     btn.addEventListener("click", (e) => {
        e.preventDefault();
        input.addEventListener("change", (e) => {
          const target = e.target as HTMLInputElement
          const files = target.files as FileList;
          const image = URL.createObjectURL(files[0])
          addImagePreview.src = image;
        });
        input.click();
      });
  })
}
uploadPicture()

 function removePicture() {

  (document.querySelector('#remove-button')as HTMLElement) .addEventListener('click', ()=>{
    input.value = ''
    addImagePreview.src = '../../images/no_image-placeholder.png'
  })
}
removePicture()
  
