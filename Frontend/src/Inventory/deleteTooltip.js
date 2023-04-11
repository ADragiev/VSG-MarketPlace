import { makeRequest } from "../global/makeRequest.js";

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
      const id = e.target.parentElement.parentElement.parentElement.parentElement.parentElement.id
      e.target.parentElement.parentElement.className = "popuptext";

      if (e.target.textContent == "YES") {
        let response = await makeRequest({
          path: "/products/" + id,
          method: "DELETE"
        });
        console.log(response);
      }
    });
  });
}
