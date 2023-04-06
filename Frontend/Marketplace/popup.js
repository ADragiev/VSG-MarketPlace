 export function showPopup() {
  const buyBtn = Array.from(document.getElementsByClassName("circle"));
  buyBtn.forEach((x) =>
    x.addEventListener("click", (e) => {
      e.target.parentElement.parentElement
        .querySelector(".popuptext")
        .classList.add("show");
    })
  );
}

export function addBtn() {
  Array.from(document.getElementsByClassName("btnYesNo")).forEach((x) => {
    x.addEventListener("click", (e) => {
        e.preventDefault()
        e.target.parentElement.parentElement.className = 'popuptext'
       
    });
  });
}

