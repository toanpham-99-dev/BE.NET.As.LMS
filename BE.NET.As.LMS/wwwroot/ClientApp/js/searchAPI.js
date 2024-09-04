var delayTimer;
function myFunction() {
  clearTimeout(delayTimer)
  delayTimer = setTimeout(function() {
    // Do the ajax stuff
    $(document).ready(function() {

      $.ajax( {
        url: "https://localhost:44333/api/1/Course/search",
        method: "GET",
        data: {
          searchName : $('#searchText').val()
        },
        dataType: 'json',
        success: function(result) {
          console.log(result.data); 
          var data = result.data;
          for (var i=0; i<data.length; i++)
          {
            for (var item in data) {
                console.log("item: ", data[item].title);
                $('#course-container-result').append(html)
                var nameCourse =$("#searchText").val();
                $('#search').html(nameCourse)
                var html= `
                  <div class="H-result-ietm">
                    <img src="https://localhost:44333/file/${data[item].imageURL}" alt="">
                    <div class="H-item-info">
                      <div class="H-course-title">
                          ${data[item].title}
                      </div>
                      <div class="H-course-text-sm">
                          Master Angular 13 (formerly "Angular 2") and build awesome, reactive web apps with the successor of Angular.js
                      </div>
                      <div class="H-course-instructor">
                          Maximilian Schwarzmüller
                      </div>
                      <div class="H-course-rate">
                          <span>4.6</span>
                          <i class="fas fa-star"></i>
                          <i class="fas fa-star"></i>
                          <i class="fas fa-star"></i>
                          <i class="fas fa-star"></i>
                          <i class="fas fa-star"></i>
                      </div>
                      <div class="H-course-info">
                          <span>34 total hours</span>
                          <span>463 lectures</span>
                          <span>All level</span>
                      </div>
                      <div class="H-course-bestSeller">
                          bestSeller
                      </div>
                    </div>
                  </div>
                  `;
            }  
          }
        },
        error: function (xhr, status, error) {
          alert("Không tìm thấy");
        }
      });
      
    })
  }, 2000);
} 
