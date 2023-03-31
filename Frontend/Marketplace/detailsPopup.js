
const modal = document.querySelector('.modal')
const productDivs = document.querySelectorAll(".product-image");
productDivs.forEach((productDiv) => {
    productDiv.addEventListener("click", ()=>{
        modal.style.display = 'block'
       

  });
});
const closeBtn = document.querySelector('.close-button')
    closeBtn.addEventListener('click', ()=>{
    modal.style.display = 'none'
})


