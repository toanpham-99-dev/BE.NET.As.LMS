$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "https://localhost:44333/api/1/Home/get-course-outstanding",
        dataType: 'json',
        data: jQuery.param({ take: 5}),
        success: function (result, status, xhr) {
            console.log(result);
            for ( course of result.data)
            {
                $("#course-container").append(
                `<div class="course__item">
                <div class="course__item__thumb" style="background-image: url(https://localhost:44333/file/${course.imageURL});"></div>
                <div class="course__item__title">
                  <a href="./course-syllabus.html">${course.title}</a>
                </div>
                <div class="course__item__studentCount">
                  <i class="far fa-users"></i> ${course.viewCount}
                </div>
              </div>`
              )
            }         
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
});
$(document).ready(function () {
 $("#search").hide();

});
$(document).ready(function () {
    $("#input-search").click(function () {
        $("#search").toggle();
    });
});
var token = localStorage.token;
token = JSON.parse(atob(token.split('.')[1]));
console.log(token);
$(document).ready(function () {
    if(token)
    {
    $(".header__login__btn").hide(); 
    $(".header__login").append(`Xin Chào ${token["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"]}`)
    $(".header__login").append(`<a href="./login.html" class="header__login__btn">Đăng Xuất</a>`)
    console.log(parseJwt);
    }
   });