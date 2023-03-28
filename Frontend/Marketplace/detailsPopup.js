function showDetailsModal() {

//   const modal = document.createElement("div");
//   modal.classList.add("modal");

//   const modalContent = document.createElement("div");
//   modalContent.classList.add("modal-content");

//   modalContent.innerHTML = `
  
//   <p>This it the description of the product.
//    This it the description of the product. 
//    This it the description of the product. 
//    This it the description of the product.
//     This it the description of the product.
//      This it the description of the product.
//       This it the description of the product.
//        This it the description of the product. 
//        This it the description of the product.
//         This it the description of the product. 
//         This it the description of the product.
//          This it the description of the product.
//    This it the description of the product. 
//    This it the description of the product.</p>
//   `

//   const productName = document.createElement("h2");
//   productName.textContent =
//     "Laptop MacBook Pro 16” M1 Max 32GB RAM 1TB SSD 32 Cores GPU";
//   modalContent.appendChild(productName);

//   const productDescription = document.createElement("p");
//   productDescription.textContent =
//     "This it the description of the product. This it the description of the product. This it the description of the product. This it the description of the product. This it the description of the product. This it the description of the product. This it the description of the product. This it the description of the product. This it the description of the product. This it the description of the product. This it the description of the product. This it the description of the product. This it the description of the product. This it the description of the product. ";
//   modalContent.appendChild(productDescription);

//   const closeButton = document.createElement("button");
//   closeButton.classList.add("close-button");
//   closeButton.textContent = "X";
//   closeButton.addEventListener("click", () => {
//     modal.remove();
//   });
//   modalContent.appendChild(closeButton);

//   modal.appendChild(modalContent);

//   document.body.appendChild(modal);
}

const modal = document.querySelector('.modal')
const productDivs = document.querySelectorAll(".product-image");
productDivs.forEach((productDiv) => {
    productDiv.addEventListener("click", ()=>{
        modal.style.display = 'block'
        // e.preventDefault()
    // showDetailsModal()

  });
});
const closeBtn = document.querySelector('.close-button')
    closeBtn.addEventListener('click', ()=>{
    modal.style.display = 'none'
})


