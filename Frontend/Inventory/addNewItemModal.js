import { makeRequest } from "../src/makeRequest.js";

const modal = document.querySelector(".add-item-modal");
const editModal = document.querySelector(".edit-item-modal");

document.getElementById("addNewItemBtn").addEventListener("click", () => {
  modal.style.display = "block";
});

const formElement = document.querySelector(".add-item-form");
formElement.onsubmit = async (e) => {
  e.preventDefault();
  let formData = Object.fromEntries(new FormData(e.target));
  let response = await makeRequest({
    path: "/products",
    method: "POST",
    data: formData,
  });
  modal.style.display = 'none'
  console.log(formData);
};

export const showEditForm = () => {
  const editIcons = document.querySelectorAll(".editIcon");
  Array.from(editIcons).forEach((i) => {
    i.addEventListener("click", () => {
      editModal.style.display = "block";
    });
  });

  const closeBtn = document.querySelectorAll(".close-modal-button");
  Array.from(closeBtn).forEach((btn) => {
    btn.addEventListener("click", () => {
      modal.style.display = "none";
      editModal.style.display = "none";
    });
  });
};

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
      const addImagePreview = document.querySelector("#addCurrentImg");
      addImagePreview.src = '../images/no_image-placeholder.png'
  })
}
removePicture()
  
