const inputField = document.getElementsByClassName("checkbox")[0];
inputField.addEventListener("change", () => {
  if (inputField.checked) {
    const userDiv = document.getElementsByClassName("user")[0];
    const main = document.getElementsByClassName("main")[0];
    main.style.display = "none";
    userDiv.className = "menu-item";
    const sidebar = document.getElementsByClassName("sidebar")[0];
    const navUl = document.querySelector("nav ul");

    navUl.prepend(userDiv);
    sidebar.style.display = "block";
    sidebar.classList.add("hamburger-aside");
  } else {
    const sidebar = document.getElementsByClassName("sidebar")[0];
    const userDiv = document.getElementsByClassName("menu-item")[0];
    const main = document.getElementsByClassName("main")[0];
    if (window.location.href.includes("inventory")) {
      main.style.display = "block";
    } else {
      main.style.display = "flex";
    }
    userDiv.className = "user";
    sidebar.style.display = "none";

    const header = document.getElementsByClassName("header")[0];
    header.appendChild(userDiv);
  }
});
