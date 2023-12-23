function changeActive(element) {
    // Bỏ lớp "active" ở tất cả các menu items
    var menuItems = document.querySelectorAll('.nav.navbar-nav.side-nav li');
    menuItems.forEach(function (item) {
        item.classList.remove('active');
    });

    // Thêm lớp "active" cho menu item được chọn
    element.parentElement.classList.add('active');
}
