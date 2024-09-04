function youtubeParse(url) {
    const regExp = /^https?\:\/\/(?:www\.youtube(?:\-nocookie)?\.com\/|m\.youtube\.com\/|youtube\.com\/)?(?:ytscreeningroom\?vi?=|youtu\.be\/|vi?\/|user\/.+\/u\/\w{1,2}\/|embed\/|watch\?(?:.*\&)?vi?=|\&vi?=|\?(?:.*\&)?vi?=)([^#\&\?\n\/<>"']*)/i;

    const match = url.match(regExp);

    return (match && match[1].length == 11) ? match[1] : false;

}
function Quiz() {
    location.href = './quiz.html';

}
function lessonDetail() {
    $.ajax({
        type: "GET",

        url: "https://localhost:44333/api/1/Course/get-lesson-detail",

        dataType: 'json',

        data: {
            courseHashCode: "1234928A0-E936-4BF0-BD08-12DF85F34979",

            lessonHashCode: "521TF81B-BE99-489F-AD23-9990F655C7D5"

        },

        headers: {
            "Content-Type": "application/json",

            "Authorization": 'Bearer '

                + localStorage.getItem('token')
        },

        success: function (result, status, xhr) {
            console.log("Function Lesson Detail", result.data)
            const data = result.data;

            const lessonData = data.lesson;

            const videoURL = youtubeParse(lessonData.linkVideo);

            $("#videoURL").html(`
    
    <iframe src="https://www.youtube.com/embed/${videoURL}"> </iframe>
    
    `)
        },

        error: function (xhr, status, error) {
            alert(error);

        }
    });

}
async function getCourse() {
    $.ajax({
        type: "GET",

        url: "https://localhost:44333/api/1/Course/get-course-detail",

        dataType: 'json',

        data: {
            courseHashCode: "1234928A0-E936-4BF0-BD08-12DF85F34979",

            // lessonHashCode: "521TF81B-BE99-489F-AD23-9990F655C7D5"

        },

        headers: {
            "Content-Type": "application/json",

            "Authorization": 'Bearer '

                + localStorage.getItem('token')
        },

        success: function (result, status, xhr) {
            lessonDetail();

            const data = result.data

            const sections = data.sections
            console.log(data)
            $(".sidebar__title").text(data.title)
            for (var section of sections) {
                var html = `
    
    <div class="learning-section">
    
    <input
    
    type="checkbox"
    
    class="learning-section__checkbox"
    
    />
    
    <label class="learning-section__label" for="check">
    
    <div class="learning-section__header">
    
    <h4 class="learning-section__header--title">
    
    ${section.description}
    
    </h4>
    
    <p class="learning-section__header--progress">
    
    1/2 | 05:44
    
    </p>
    
    </div>
    
    </label>
    
    <div id = "${section.hashCode}" class="content">
    
    </div>
    
    `
                $("#listCourse").append(html)
                for (var item of section.lesson) {
                    console.log(item.hashCode)
                    var htmlLesson = `

<div class="content__lesson">

<div class="content__title-box">

<div class="content__icon-box">

<i class="fas fa-lock content__icon-lock"></i>

</div>

<div class="content__text-box">

<p class="content__text-header">

${item.name}

</p>

<p class="content__play-box">

<i class="fas fa-play-circle content__icon-play"></i>

<span class="content__video-length">${item.duration}</span>

</p>

</div>

</div>

<div class="content__exercise">

<h3 class="content__exercise-text">Bài tập</h3>

<div id = "excercise${item.hashCode}" class="content__btn-box">

</div>

</div>

</div>

</div>

`;

                    $(`#${section.hashCode}`).append(htmlLesson)
                    $.ajax({
                        type: "GET",

                        url: "https://localhost:44333/api/1/Course/get-lesson-detail",

                        dataType: 'json',

                        data: {
                            courseHashCode: "1234928A0-E936-4BF0-BD08-12DF85F34979",

                            lessonHashCode: item.hashCode
                        },

                        headers: {
                            "Content-Type": "application/json",

                            "Authorization": 'Bearer '

                                + localStorage.getItem('token')
                        },

                        success: function (result, status, xhr) {
                            const hashCodeLesson = result.data.lesson
                            const assignments = result.data.assignments
                            const index = 0;

                            for (const item of assignments) {
                                console.log(item)
                                var htmlQuiz = `

<button class="content__btn" onclick="Quiz()">${item.assignmentName.slice(16, 18)}</button>

`

                                $(`#excercise${hashCodeLesson.hashCode}`).append(htmlQuiz)
                            }
                        },

                        error: function (xhr, status, error) {
                            alert(error);

                        }
                    });

                }
            }
        },

        error: function (xhr, status, error) {
            alert(error);

        }
    });

}
getCourse()