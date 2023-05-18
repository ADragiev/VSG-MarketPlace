// const inputField = document.getElementsByClassName("checkbox")[0] as HTMLInputElement;
// inputField.addEventListener("change", () => {
//   if (inputField.checked) {
//     const userDiv = document.getElementsByClassName("user")[0] as HTMLElement;
//     const main = document.getElementsByClassName("main")[0] as HTMLElement;
//     main.style.position = "fixed";
//     main.style.right = "-100%";
//     userDiv.className = "menu-item";
//     const sidebar = document.getElementsByClassName("sidebar")[0] as HTMLElement;
//     const navUl = document.querySelector("nav ul") as HTMLElement;

//     navUl.prepend(userDiv);
//     sidebar.style.display = "block";
//     sidebar.classList.add("hamburger-aside");
//   } else {
//     const sidebar = document.getElementsByClassName("sidebar")[0] as HTMLElement;
//     const userDiv = document.getElementsByClassName("menu-item")[0] as HTMLElement;
//     const main = document.getElementsByClassName("main")[0] as HTMLElement;
//     if (window.location.href.includes("inventory")) {
//         main.style.position = "fixed";
//       main.style.right = "0";
//     } else {
//      main.style.position = "fixed";
//       main.style.right = "0";
//     }
//     userDiv.className = "user";
//     sidebar.style.display = "none";

//     const header = document.getElementsByClassName("header")[0] as HTMLElement;
//     header.appendChild(userDiv);
//   }
// });