export function showConfirmRejectPopup(div) {
  const deleteBtn = Array.from(div.getElementsByClassName("deleteIcon"));
  deleteBtn.forEach((x) =>
    x.addEventListener("click", (e) => {
      e.target.parentElement.querySelector(".popuptext").classList.add("show");
    })
  );
}

export function confirmBtn(div) {
  div.querySelectorAll(".btnYesNo").forEach((x) => {
    x.addEventListener("click", async (e) => {
      // e.preventDefault()
      e.target.parentElement.parentElement.className = "popuptext";

      let orderId =
        e.target.parentElement.parentElement.parentElement.parentElement.id;
      if (e.target.textContent == "YES") {
        await fetch(`https://localhost:7054/Order/${orderId}`, {
          method: "DELETE",
        });
        location.reload();
      }
    });
  });
}
