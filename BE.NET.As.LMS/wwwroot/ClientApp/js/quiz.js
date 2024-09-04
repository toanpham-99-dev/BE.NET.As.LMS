$(document).ready(function () {
    $.ajax({
      type: "GET",
      url: "https://localhost:44333/api/1/Quiz?",
      dataType: 'json',
      data: jQuery.param({ hashcode: "521TF81B-BE99-489F-AD23-9990F655C7D5" }),
      success: function (lesson, status, xhr) {
        for (quiz of lesson.data) {
          for(answers of quiz.answers){
            $("#answer-content").append(
              `
                  <div class="quiz__quiz-result-box quiz-option">
                            <div class="quiz__quiz-result-option">
                                <label class="quiz__quiz-checkbox-option">
                                    <input type="checkbox">
                                  </label>
                                <div class="quiz__quiz-content">
                                    <p>${answers.answerContent}</p>
                                </div>
                            </div>
                        </div>
                  `
            )
          }
        }
      },
      error: function (xhr, status, error) {
        alert(error);
      }
    });
  });
  
  
  
  $(document).ready(function () {
    $.ajax({
      type: "GET",
      url: "https://localhost:44333/api/1/Quiz?",
      dataType: 'json',
      data: jQuery.param({ hashcode: "521TF81B-BE99-489F-AD23-9990F655C7D5" }),
      success: function (lesson, status, xhr) {
        for (quiz of lesson.data) {
          $("#quiz-content").append(
            `
               <p class="quiz__quiz-question-text">Cau hoi</p>
               <p>${quiz.quizContent}</p>
            `
          )
        }
      },
      error: function (xhr, status, error) {
        alert(error);
      }
    });
  });
  
  
  