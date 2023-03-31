const modal = document.querySelector(".add-item-modal");
const editModal = document.querySelector(".edit-item-modal");

document.getElementById("addNewItemBtn").addEventListener("click", () => {
  modal.style.display = "block";
});

const editIcons = document.querySelectorAll(".editIcon")
Array.from(editIcons).forEach(i => {
    i.addEventListener("click", () => {
        editModal.style.display = "block";
    })
});

const closeBtn = document.querySelectorAll(".close-modal-button");
Array.from(closeBtn).forEach(btn => {
btn.addEventListener("click", () => {
    modal.style.display = "none";
    editModal.style.display = "none"
  })
});




// function uploadPicture() {
//   const addImagePreview = document.querySelector("#addCurrentImg");
//   const editImagePreview = document.querySelector("#editCurrentImg");

//   document.querySelectorAll("upload-button").forEach(btn =>{
//      btn.addEventListener("click", (e) => {
//         e.preventDefault();
//         const input = document.createElement("input");
//         input.type = "file";
//         input.addEventListener("change", () => {
//           const file = input.files[0];
//           const reader = new FileReader();
//           reader.onload = function (event) {
//             addImagePreview.src = event.target.result;
//             editImagePreview.src = event.target.result;

//           };
//           reader.readAsDataURL(file); 
//         });
//         input.click();
//       });
//   })
// }

// function removePicture() {
    
//     const addImagePreview = document.querySelector("#addCurrentImg");
//     addImagePreview.src = '../images/no_image-placeholder.png'
//     const editImagePreview = document.querySelector("#editCurrentImg");
//     editImagePreview.src = '../images/no_image-placeholder.png'
//   }