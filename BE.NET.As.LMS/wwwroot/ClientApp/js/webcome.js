$(document).ready(function () {
    var url = "https://localhost:44333/"
    $("#login").click(function () {
        $("#login").attr("href", "./login.html")
    });
    $("#login-facebook").click(function () {
        $("#login-facebook").attr("href", `${url}api/1/Authentication/facebook-login`)      
    });
    $("#login-google").click(function () {
        $("#login-google").attr("href", `${url}api/1/Authentication/google-login`)
    });
    $.ajax({
        type: "GET",
        url: "https://localhost:44333/api/1/Authentication/call-back",
        dataType: 'json',
        success: function (result, status, xhr) {
            console.log(result);              
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    }); 
});