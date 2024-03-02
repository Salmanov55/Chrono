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


const slides = document.querySelectorAll('.swiper-slide');
const next = document.getElementById('next');
const prev = document.getElementById('prev');

const nextSlide = () => {
  const activeSlide = document.querySelector('.active');
  activeSlide.classList.remove('active');

  if (activeSlide.nextElementSibling) {
    activeSlide.nextElementSibling.classList.add('active');
  } else {
    slides[0].classList.add('active');
  }
};

const prevSlide = () => {
  const activeSlide = document.querySelector('.active');
  activeSlide.classList.remove('active');

  if (activeSlide.previousElementSibling) {
    activeSlide.previousElementSibling.classList.add('active');
  } else {
    slides[slides.length - 1].classList.add('active');
  }
};

next.addEventListener('click', nextSlide);
prev.addEventListener('click', prevSlide);



var swiper = new Swiper(".mySwiper", {
  slidesPerView: 2,
  spaceBetween: 30,
  keyboard: {
    enabled: true,
  },
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },
});



var swiper = new Swiper(".mySwiper", {
  slidesPerView: 5,
  spaceBetween: 30,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
});


document.addEventListener("DOMContentLoaded", function() {
  const tabLinks = document.querySelectorAll('.tablinks');
  tabLinks.forEach(function(tabLink) {
    tabLink.addEventListener('click', function() {
      tabLinks.forEach(function(link) {
        link.classList.remove('active');
      });
      this.classList.add('active');
    });
  });
});

document.addEventListener("DOMContentLoaded", function () {
  const classicsTab = document.querySelector('.tablinks:nth-child(1)');
  const modernTab = document.querySelector('.tablinks:nth-child(2)');
  const classicCardList = document.querySelector('.card-list[data-tab="dT_tab-1"]');
  const modernCardList = document.querySelector('.card-list[data-tab="dT_tab-2"]');
  const saleClock = document.querySelector('.sale-clock');
  
  classicCardList.classList.add('active');
  classicCardList.style.display = 'flex';
  
  classicsTab.addEventListener('click', function () {
      classicsTab.classList.add('active');
      modernTab.classList.remove('active');
      classicCardList.classList.add('active');
      classicCardList.style.display = 'flex';
      
      modernCardList.classList.remove('active');
      modernCardList.style.display = 'none';

      saleClock.appendChild(modernCardList);
  });
  
  modernTab.addEventListener('click', function () {
      classicsTab.classList.remove('active');
      modernTab.classList.add('active');
      classicCardList.classList.remove('active');
      classicCardList.style.display = 'none';
      
      modernCardList.classList.add('active');
      modernCardList.style.display = 'flex';
  });
});


var swiper = new Swiper(".mySwiper", {
  slidesPerView: 3,
  centeredSlides: true,
  loop: true,
  slidesPerGroup: 3,
  grabCursor: true,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
  navigation: {
    prevEl: '.swiper-button-prev',
    nextEl: '.swiper-button-next'
  }
});