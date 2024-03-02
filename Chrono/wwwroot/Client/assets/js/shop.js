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

document.addEventListener("DOMContentLoaded", function () {
    var dropdowns = document.querySelectorAll(".filter__dropdown");

    dropdowns.forEach(function (dropdown) {
        dropdown.addEventListener("click", function (event) {
            event.stopPropagation(); 
            dropdown.classList.toggle("active");
        });

        
        var content = dropdown.querySelector(".filter__dropdown-content");
        if (content) {
            content.addEventListener("click", function (event) {
                event.stopPropagation(); 
            });
        }
    });

   
    document.addEventListener("click", function () {
        dropdowns.forEach(function (dropdown) {
            dropdown.classList.remove("active");
        });
    });
});
function toggleSubMenu(submenuId) {
    var submenu = document.getElementById(submenuId);
    var pagesMenu = document.getElementById("pagesMenu");
    var plusSign = pagesMenu.querySelector(".plus");
    var minusSign = pagesMenu.querySelector(".minus");

    submenu.classList.toggle("show");

    if (submenu.classList.contains("show")) {
        plusSign.style.display = "none";
        minusSign.style.display = "inline";
    } else {
        plusSign.style.display = "inline";
        minusSign.style.display = "none";
    }
}
