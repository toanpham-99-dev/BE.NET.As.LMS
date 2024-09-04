$(document).ready(function() {
    localStorage.clear();
    var url = "https://localhost:44333/"
    $("#btn-login").click(function(){
        $.ajax({
            type: "POST",
            url: "https://localhost:44333/api/1/Authentication/login",         
            dataType: 'json',
            data:({
                UserName : $('#userName').val(),
                Password: $('#password').val()
            }),
            success: function (result, status, xhr) {
                alert(result.message);
                localStorage.setItem("token", result.data)
                headers: {
                    Authorization: 'Bearer ' + result.data
                }; 
                location.href='./home-page.html';           
            },
            error: function (xhr, status, error) {
                 alert("Có lỗi xảy ra khi đăng nhập!");
            }
        });
      });
    });
   