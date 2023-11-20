$(document).ready(function () {
    $("#activeMenu li").click(function () {
        $(this).addClass("active");
    });

});


$(document).ready(function () {
    $("#activeMenu li").click(function () {
        console.log("Clicked!"); // Kiểm tra xem sự kiện click có được kích hoạt không
        $("#activeMenu li").removeClass("active");
        $(this).addClass("active");
    });
});
