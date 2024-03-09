// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var avatar = document.querySelector(".avartar");
var user_option = document.querySelector(".user_option");
var closeOptionBtn = document.querySelector(".top_user_option i");

avatar.addEventListener("click", function () {
  user_option.classList.toggle("show_user_option");
});

closeOptionBtn.addEventListener("click", function () {
  user_option.classList.remove("show_user_option");
});
