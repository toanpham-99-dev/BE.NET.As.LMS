let orderHashCode = '';
$(document).ready(function() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: 'https://localhost:44333/api/1/Order/get-all-order-by-current-user-hash-code',
        headers: { Authorization: 'Bearer ' + localStorage.getItem("token") },
        success: function (response) {
            loadCartDetail(response);
            let listCategoryOfCourseInOrder = getCourseInOrder(response);
            loadCategories(listCategoryOfCourseInOrder);
        },
        error: function (param) {
            console.log(param.responseJSON);
        }
    });
});
function loadCartDetail(response){
    $('.go_to_cart_info').html("");
    var order = response.data[0];
    orderHashCode = order.hashCode;
    var orderDetails = order.orderDetails;
    for(const item of orderDetails){
        var htmlRow = 
        `<div class='go_to_cart_info'>
        <i class="bi bi-check-circle-fill cart-check-icon" style="color: green; font-size:30px"></i>
        <div class="item_info">
            <img src="https://img-c.udemycdn.com/course/100x100/2201164_831a.jpg" alt="cart_item_image">
            <div class='cart-item-text'>
                <div style="font-weight: bold;">${item.courseName}</div>
                <div>Price : ${item.price}$</div>
            </div>
        </div>
        </div>`;
        $('.cart_active_header').after(htmlRow);
        $('#total_cart_quantity').html(order.quantity);
        $('#total_cart_price').html("$"+ order.totalPrice);
    }
}
function getCourseInOrder(response){
    let categoryNames = [];
    let order = response.data[0];
    let orderDetails = order.orderDetails;
    for(const item of orderDetails){
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: 'https://localhost:44333/api/1/Course/get-guest-course-detail',
            data: {
                hashCode: item.courseHashCode,
            },
            success: function (response) {
                categoryNames.push(response.data.categoryName);
            },
            error: function (param) {
                console.log(param.responseJSON);
            }
        }); 
    }
    return categoryNames;
}
function loadOrtherBestCourse(categoryHashCode){
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: 'https://localhost:44333/api/1/Course/get-courses-by-category',
        data: {
            hashCode: categoryHashCode,
        },
        success: function (response) {
            $('.group_item').html("");
            for(const item of response.data){
                var htmlRow = `<a href="./course-syllabus.html">
                <div class="cart_course_info">
                    <div class="cart_detail_info">
                        <div class="course_image">
                            <img src="https://localhost:44333/file/${item.imageURL}" alt="item-image" width="220" height="115">
                        </div>
                        <div class="cart_content_item">
                            <div class="items_align">
                                <div class="items_text_info">
                                    <div style="font-weight: bold;" class="course_title">${item.title}</div>
                                    <div class='course_description'>${item.createdBy}</div>
                                    <div class="course_rating">
                                        <span class="rating_point">${item.rating}</span>
                                        <span class="fa fa-star ${item.rating >= 1? "checked": ""}"></span>
                                        <span class="fa fa-star ${item.rating >= 2? "checked": ""}"></span>
                                        <span class="fa fa-star ${item.rating >= 3? "checked": ""}"></span>
                                        <span class="fa fa-star ${item.rating >= 4? "checked": ""}"></span>
                                        <span class="fa fa-star ${item.rating == 5? "checked": ""}"></span>
                                        <span class="rating_count_register">${item.numberOfStudent}</span>
                                </div>
                        </div>
                            <div class="course_price_item">
                                <div class="discount_prices">$${item.price}</div>
                            </div>
                        </div>
                    </div>
                </div>
                </a>`;
                $('.group_item').append(htmlRow);
            }
        },
        error: function (param) {
            console.log(param.responseJSON);
        }
    });
}
function loadCategories(listCategoryOfCourseInOrder){
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: 'https://localhost:44333/api/1/Category/get-all-row-categories',
        success: function (response) {
            $('.other_topic').html("");
            console.log(listCategoryOfCourseInOrder[listCategoryOfCourseInOrder.length-1]);
            for(const item of response.data){
                if(!listCategoryOfCourseInOrder.includes(item.title)){
                    var htmlRow = `<a href="./course-syllabus.html" <span>${item.title}</span></a>`;
                    $('.other_topic').append(htmlRow);
                    loadOrtherBestCourse(item.hashCode);
                }
            }
        },
        error: function (param) {
            console.log(param.responseJSON);
        }
    });
}
function payOrder(){
    $.ajax({
        type: 'PUT',
        dataType: 'json',
        url: `https://localhost:44333/api/1/Order/paid-or-cancel-order?orderHashCode=${orderHashCode}&orderStatus=1`,
        headers: { Authorization: 'Bearer ' + localStorage.getItem("token"), 'Content-Type': 'application/json',},
        success: function () {
            alert("Bạn đã thanh toán");
        },
        error: function (param) {
            console.log(param.responseJSON);
        }
    });
}