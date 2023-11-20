let searchBtn = document.querySelector(".bx-search");
searchBtn.addEventListener("click", () => {
    // Sidebar open when you click on the search iocn
    sidebar.classList.toggle("open");
    menuBtnChange(); //calling the function(optional)
});
