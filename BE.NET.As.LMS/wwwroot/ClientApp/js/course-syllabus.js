var token = localStorage.token;
token = JSON.parse(atob(token.split('.')[1]));
if (token) {
    $(".header__login__btn").hide();
    $(".header__login").append(`Xin Chào ${token["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"]}`)
    $(".header__login").append(`<a href="./login.html" class="header__login__btn">Đăng Xuất</a>`)
}
async function getCourseDetail(hashCode) {
    await $.ajax({
        url: 'https://localhost:44333/api/1.0/Course/get-guest-course-detail',
        type: 'GET',
        data: { hashCode: hashCode },
        dataType: "json",
        headers: { Authorization: 'Bearer ' + localStorage.getItem("token") },

        success: function (response) {
            //add Title Cousre & description course
            $("#course_title").html(response.data.title);
            $("#course_summary").html(response.data.summary);
            //add description details html
            $("#course_description").html(response.data.content);
            // descriptionDetail(data.data.descriptionDetail);
            infoSectionTop(response.data);
            //add list lession html
            sectionDetail(response.data.section);
            //info course right
            infoSectionRight(response.data);
        },
        error: function () {
            alert("Data is invalid");
        }
    });
    await $("#btn-register").click(function () {
        var hashCode = "1234928A0-E936-4BF0-BD08-12DF85F34979";
        $.ajax({
            type: "POST",
            url: "https://localhost:44333/api/1/User/user-enroll?courseHashCode=1234928A0-E936-4BF0-BD08-12DF85F34979",
            dataType: 'json',
            data: ({ }),
            headers: { Authorization: 'Bearer ' + localStorage.getItem("token") },
            success: function (result, status, xhr) {
                alert("Đăng ký khóa học thành công");
                location.href='./course.html'; 
            },
            error: function (xhr, status, error) {
                alert("Có lỗi xảy ra đăng ký học");
            }
        });
    });
    
}
function sectionDetail(response) {
    var index = 1;
    response.forEach(element => {
        var htmlRow = `<div class="learning-section">
    <input
      type="checkbox"
      id="check${index}"
      class="learning-section__checkbox"
    />
    <label class="learning-section__label" for="check${index}">
      <div class="learning-section__header">
        <h4 class="learning-section__header--title">
        ${index}. ${element.description}
        </h4>
        <p class="learning-section__header--lessons">
          ${element.lesson.length} Bài học
        </p>
      </div>
    </label>
  </div>`
        $("#syllabus_content").append(htmlRow);
        element.lesson.forEach(item => {
            var htmlRow = `
        <div id="content_lesson" class="content">
            <div class="content__lesson">
                <div class="content__text-box">
                    <i class="fas fa-play-circle content__icon-play"></i>
                    <p class="content__title">${item.name}</p>
                </div>
                    <p class="content__time-length">${item.duration}</p>
            </div>
        </div>
        `
            $("#syllabus_content").append(htmlRow);
        });
        index++;
    });
}
function convertTotalTime(time) {

    var totalTime = time.split(":");
    var time = totalTime[0] + " Giờ " + totalTime[1] + " Phút " + totalTime[2] + " Giây";
    return time;
}
function infoSectionTop(response) {
    var totalTime = sumTotalTimeLession(response);
    var time = convertTotalTime(totalTime);
    var totalLession = sumTotalLession(response);
    var htmlRaw = `
      <p class="syllabus__modules">${response.section.length} Phần</p>
      <p class="syllabus__lessons">${totalLession} Bài học</p>
      <p class="syllabus__length">
          Thời lượng ${time}
      </p>
  `;
    $("#syllabus-infor-container").append(htmlRaw);
}
function infoSectionRight(response) {
    var htmlRawIntroVideos = `
      <iframe class="side-content__video"
          src="https://www.youtube.com/embed/"
          title="YouTube video player"
          frameborder="0"
          allow="accelerometer; autoplay="true"; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
          allowfullscreen>
      </iframe>
  `;
    $("#video-intro").html(htmlRawIntroVideos);

    //button enroll or buy course
    var htmlRawStatusCourseFree = `
      <p class="side-content__text--free">Miễn phí</p>
      <a href="#" id="btn-register"class="side-content__btn-register">
          Đăng ký học
      </a>
   `;
    var money = response.price.toLocaleString('en-US', {
        style: 'currency',
        currency: 'USD',
    });
    var htmlRawStatusCourseBuy = `
      <p class="side-content__text--free">${money}</p>
      <a onclick="alert('Mua khóa học')"  href="#" class="side-content__btn-register">
      Mua ngay
      </a>
   `;
    var htmlRawStatusCourseStudy = `
        <p></p>
        <a onclick="alert('Tiếp tục học')"  href="../pages/course.html?courseHashCode=${response.hashCode}" class="side-content__btn-register">
            Tiết tục học
        </a>
     `;
    (response.price > 0) ? $("#status-course").html(htmlRawStatusCourseBuy) : $("#status-course").html(htmlRawStatusCourseFree);
    // Check bought course?
    if (token != undefined) {
        console.log(token)
        var userInfo = localStorage.token;
        $.ajax({
            url: 'https://localhost:44333/api/1/Course/get-course-user-enroll',
            type: "GET",
            dataType: "json",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + localStorage.token
            },
            success: function (result) {
                console.log(result.data);
                result.data.forEach(element => {
                    if (element.hash == result.hash) {
                        $("#status-course").html(htmlRawStatusCourseStudy);
                    }
                });
            },
            error: function (param) {
                console.log(param.responseJSON);
                alert("Data is invalid");
            }
        });
    }

    // level
    var level = response.level;
    switch (level) {
        case 1:
            $("#level").html("Cơ bản")
            break;
        case 2:
            $("#level").html("Nâng cao")
            break;
        default:
            break;
    }

    // total lession
    var totalLession = sumTotalLession(response);
    $("#totalLession").html(totalLession + " Bài học");

    var totalTime = sumTotalTimeLession(response);
    var time = convertTotalTime(totalTime);
    $("#totalTime").html("Thời lượng " + time);

}

function sumTotalTimeLession(response) {
    var timeSpan = 0;
    var result = 0;
    var timePart = 0;
    var milliseconds = (h, m, s) => ((h * 60 * 60 + m * 60 + s) * 1000);
    response.section.forEach(element => {
        element.lesson.forEach(item => {
            timeSpan = item.duration;
            timePart = timeSpan.split(":");
            result += milliseconds(timePart[0], timePart[1], timePart[2]);
        });
    });
    return parseMillisecondsIntoReadableTime(result / 100);
}
function sumTotalLession(response) {
    var totalLession = 0;
    response.section.forEach(element => {
        totalLession += element.lesson.length;
    });
    return totalLession;
}
function parseMillisecondsIntoReadableTime(milliseconds) {
    //Get hours from milliseconds
    var hours = milliseconds / (1000 * 60 * 60);
    var absoluteHours = Math.floor(hours);
    var h = absoluteHours > 9 ? absoluteHours : '0' + absoluteHours;

    //Get remainder from hours and convert to minutes
    var minutes = (hours - absoluteHours) * 60;
    var absoluteMinutes = Math.floor(minutes);
    var m = absoluteMinutes > 9 ? absoluteMinutes : '0' + absoluteMinutes;

    //Get remainder from minutes and convert to seconds
    var seconds = (minutes - absoluteMinutes) * 60;
    var absoluteSeconds = Math.floor(seconds);
    var s = absoluteSeconds > 9 ? absoluteSeconds : '0' + absoluteSeconds;

    return h + ':' + m + ':' + s;
}

