
let slideIndex = 1;
let slideTimer;

showSlides(slideIndex);
startAutoSlide();

function plusSlides(n) {
  showSlides(slideIndex + n);
  startAutoSlide();
}

function currentSlide(n) {
  showSlides(n);
  startAutoSlide();
}

function showSlides(n) {
  const slides = document.getElementsByClassName("mySlides");
  const dots = document.getElementsByClassName("dot");

  if (n > slides.length) {
    slideIndex = 1;
  }

  if (n < 1) {
    slideIndex = slides.length;
  }

  for (let i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";
  }

  for (let i = 0; i < dots.length; i++) {
    dots[i].classList.remove("active");
  }

  slides[slideIndex - 1].style.display = "block";
  dots[slideIndex - 1].classList.add("active");
}

function startAutoSlide() {
  clearInterval(slideTimer);

  slideTimer = setInterval(function () {
    showSlides(slideIndex + 1);
  }, 3000);
}
