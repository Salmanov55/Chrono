let nav_toggle = document.querySelector(".nav_toggle");
let nav_toggle_icon = document.querySelector(".nav_toggle ion-icon");
let nav_menu = document.querySelector(".nav_menu");

nav_toggle.addEventListener("click", () => {
  nav_menu.classList.toggle("active");
  nav_toggle_icon.setAttribute(
    "name",
    nav_menu.classList.contains("active") ? "close-outline" : "menu-outline"
  );
});

$(document).ready(function () {
    $(".slider").slick({
        arrows: false,
        dots: false,
        slidesToShow: 1,
        autoplay: true,
        speed: 1000,
        autoplaySpeed: 800,
        responsive: [
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 2,
                },
            },
            {
                breakpoint: 550,
                settings: {
                    slidesToShow: 1,
                },
            },
        ],
    });
});