// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getData(id) {
  var e = document.getElementById(id);
  var chosenData = e.options[e.selectedIndex].text;
  console.log(chosenData);
}

function getRealEstate() {
  $.get("Listings/Details", { id_real_estate: "1" });
}
