export const header = () =>{
    const title = document.querySelector('title').textContent
    const header = document.querySelector('.header')
    header.innerHTML = `
    <a href="/index.html">
        <img src="../../images/vsg_marketplace-mini-logo 1.jpg" alt="mini-logo" />
      </a>
      <span>${title}</span>
      <div id="greetingContainer" class="user">
        <span> Hi, user! </span>
        <img src="../../images/Profile Img.jpg" alt="Profile-pic" />
      </div>
      <div class="hamburger-icon-container">
        <div class="hamburger-lines">
          <input class="checkbox" type="checkbox" name="" id="" />
          <span class="line line1"></span>
          <span class="line line2"></span>
          <span class="line line3"></span>
        </div>
      </div>
    `
}
header()  