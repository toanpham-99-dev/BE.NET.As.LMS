$(document).ready(function() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: 'https://localhost:44333/api/1/Notification/getall-by-user-hash-code',
        headers: { Authorization: 'Bearer ' + localStorage.getItem("token") },
        success: function (response) {
            loadNotifications(response);
            displayNewNotificationNumber(response);
        },
        error: function (param) {
            console.log(param.responseJSON);
        }
    });
});
function loadNotifications(response){
    $('.notification_items').html("");
    for(const item of response.data){
        var htmlRow = 
        `<div class="notification_items">
        <ul>
            <li class="notification_content" id = "${item.hashCode}" onclick="readNotification(this)">
                <div class=notification_profile_picture>
                    <img src="https://img-c.udemycdn.com/course/100x100/2201164_831a.jpg" alt="">
                </div>
                <div class="notification_content_detail">
                    <div class="notification_message_content">
                        <span class="name">${item.content}</span>
                    </div>
                    <div class="notication_timeline">${formatDate(item.createdAt)}</div>
                </div>
            </li>
        </ul>
        </div>`;
        $('.notification_box').append(htmlRow);
        if(item.isRead == false)
            $('.name').last().css("font-weight", "bold");
    };  
}
function displayNewNotificationNumber(response){
    var number = response.data.filter(_ => _.isRead == false).length;
    $(".notifcation_title h5:first").append(`(${number})`);
}
function getDifferentntDate(date){
    date = new Date(date);
    currentDate = new Date();
    return Math.abs(currentDate.getTime() - date.getTime());
}
function formatDate(date){
    var date = new Date(date);
    return `Lúc ${date.toLocaleTimeString()} ngày ${date.toLocaleDateString()}`;
}
function readNotification(item) {
    var id = $(item).attr('id');
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: `https://localhost:44333/api/1/Notification/get-notification`,
        headers: { Authorization: 'Bearer ' + localStorage.getItem("token") },
        data: {
            hashCode: id,
        },
        success: function (response) {
            result = window.confirm("Bạn có muốn đi đến trang :" + response.data.link);
            if(result == true)
                location.href = response.data.link;
            if(response.data.isRead == false)
                updateIsReadStatus(response.data);
        },
        error: function (param) {
            console.log(param.responseJSON);
        }
    });
}    
function updateIsReadStatus(data){
    $.ajax({
        type: 'PUT',
        dataType: 'json',
        url: `https://localhost:44333/api/1/Notification/update-inline-notification`,
        headers: 
        { 
            Authorization: 'Bearer ' + localStorage.getItem("token"),
            'Content-Type': 'application/json', 
            'charset': 'utf-8'
        },
        data: JSON.stringify({
            "hashCode": data.hashCode,
            "content": data.content,
            "link": data.link,
            "isRead": true,
            "status": data.status,
            "createdAt": data.createdAt
        }),
        success: function () {
            $("#" + data.hashCode + " span:first").css("font-weight", "normal");
            console.log("Xin chao")
            updateNewNotificationNumber();
        },
        error: function (param) {
            console.log(param.responseJSON);
        }
    });
}
function updateNewNotificationNumber(){
    let text = $(".notifcation_title h5:first").text();
    let number = text.substring(14, text.length - 1);
    number -= 1;
    if(number == 0)
        text = text.slice(0, 13);
    else
        text = text.slice(0, 14) + number + ")";
    $(".notifcation_title h5:first").html(text);
}