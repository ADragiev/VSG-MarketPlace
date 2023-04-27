export function showDeletePopup() {
  const deleteBtn = Array.from(document.getElementsByClassName("deleteIcon"));
  deleteBtn.forEach((x) =>
    x.addEventListener("click", (e) => {
      e.target.parentElement.parentElement.parentElement
        .querySelector(".popuptext")
        .classList.add("show");
    })
  );
}

export function addBtn() {
  Array.from(document.getElementsByClassName("btnYesNo")).forEach((x) => {
    x.addEventListener("click", async (e) => {
      e.preventDefault();
      console.log(x);
      const id =
        e.target.parentElement.parentElement.parentElement.parentElement
          .parentElement.id;
      e.target.parentElement.parentElement.className = "popuptext";
      if (e.target.textContent == "YES") {
        let response = await fetch(`https://localhost:7054/Product/${id}`, {
          method: "DELETE",
        });
        location.reload();
        console.log(response);
      }
    });
  });
}
