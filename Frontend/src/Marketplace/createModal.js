
export const createModal = async (product) =>{

    const modal = document.querySelector('.modal')
    // const product = await loadProductsDetails(id)
    const modalContent = document.createElement('div')
    modalContent.className = 'modal-content'

    modalContent.innerHTML = `
    <a class="close-button">
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
    <a class="productImage">
    <img
   
    src="${product.image ? product.image : `../../images/no_image-placeholder.png`}"
    alt="ProductImage"
  />
    </a>
   
    <section>
      <div class="details-section">
        <div class="name-details">
          <span
            >${product.name}</span
          >
          <small>${product.category}</small>
        </div>
        <div class="other-details">
          <span>${product.price} BGN</span>
          <small>Qty:1</small>
        </div>
      </div>

      <p>
       ${product.description}
      </p>
      <svg
        id="circles"
        width="293"
        height="136"
        viewBox="0 0 293 136"
        fill="none"
        xmlns="http://www.w3.org/2000/svg"
      >
        <circle
          cx="245.5"
          cy="156.5"
          r="156.5"
          fill="#ED1C25"
          fill-opacity="0.04"
        />
        <circle
          cx="88.5"
          cy="61.5"
          r="18.5"
          fill="#ED1C25"
          fill-opacity="0.04"
        />
        <circle
          cx="41"
          cy="128"
          r="41"
          fill="#ED1C25"
          fill-opacity="0.04"
        />
      </svg>
    </section>
    `

    modal.appendChild(modalContent)
}