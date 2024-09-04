using System.Collections.Generic;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class StatisticOutput
    {
        public int NumberOfAdmin { get; set; }
        public int NumberOfUser { get; set; }
        public int NumberOfInstructor { get; set; }
        public int NumberOfCategory { get; set; }
        public int NumberOfCourse { get; set; }
        public int NumberOfLesson { get; set; }
        public List<string> Top5RatingCourses { get; set; }
        public List<string> Top5Students { get; set; }
    }
}
