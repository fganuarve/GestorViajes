// Initialize swiper.js for main slider

var swiper = new Swiper(".swiper", {
  slidesPerView: 1,
  loop: true,
  // If we need pagination
  pagination: {
    el: ".swiper-pagination",
    renderBullet: function (index, className) {
      return '<li class="' + className + '"></li>';
    },
    clickable: true,
  },
});

const passwordField = document.querySelector(".password");
const eyeIcon = document.querySelector("#icon");

eyeIcon.addEventListener("click", function () {
  const type =
    passwordField.getAttribute("type") === "password" ? "text" : "password";
  passwordField.setAttribute("type", type);
  eyeIcon.classList.toggle("bx-hide");
  eyeIcon.classList.toggle("bx-show");
});
