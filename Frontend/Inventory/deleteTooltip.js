function myFunction() {
    const deleteBtn = Array.from(document.getElementsByClassName("deleteIcon"));
    deleteBtn.forEach((x) =>
      x.addEventListener("click", (e) => {
        console.log(e.target);
        e.target.parentElement.parentElement.parentElement
          .querySelector(".popuptext")
           .classList.add("show");
      })
    );
  }

  function addBtn() {
    Array.from(document.getElementsByClassName("btnYesNo")).forEach((x) => {
      x.addEventListener("click", (e) => {
          e.preventDefault()
          e.target.parentElement.parentElement.className = 'popuptext'
         
      });
    });
  }
  
  addBtn();
  myFunction()