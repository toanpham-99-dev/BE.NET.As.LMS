using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BE.NET.As.LMS.Utilities.Constaint;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface ICourseServices
    {
        Task<Course> GetByHashCode(string hashcode);
        Task<List<SearchCourseOutput>> SearchByName(string searchName);
        Task<int> CreateCourse(CourseInput courseInput, string userHashCode);
        Task<int> UpdateCourse(string courseHashCode, UpdateCourseInput updatecourseInput, string userHashCode);
        Task<int> DeleteCourse(string courseHashCode, string userHashCode);
        Task<SyllabusOfCourseOutput> GetSyllabusOfCourse(string hashCode);
        Task<RatingOfCourseOutput> GetRatingOfCourse(string hashCode);
        Task<GuestCourseDetailOutput> GuestCourseDetail(string hashCode);
        Task<List<SearchCourseOutput>> SearchByPrice(decimal searchPrice, int pageSize, int pageIndex);
        Task<List<SearchCourseOutput>> SearchByLevel(EnumCourseLevel searchLevel, int pageSize, int pageIndex);
        Task<List<SearchCourseOutput>> SearchByInstructor(string searchString, int pageSize, int pageIndex);
        Task<List<SearchCategoryOutput>> SearchByCategory(string searchString, int pageSize, int pageIndex);
        Task<List<CourseAdminOutput>> GetAllPagingFilterSearchCourse(string searchString, EnumCourseLevel level, int pageSize, int pageIndex);
        Task<List<CourseByCategoryOutput>> GetCoursesByCategory(string hashCode);
        Task<int> PublicOrHideCourse(string courseHashCode, CourseStatus courseStatus);
        Task<LessonDetailOutput> LessonDetail(string courseHashCode, string userHashCode, string lessonHashCode);
        Task<UserCourseDetailOutput> UserCourseDetail(string courseHashCode);
        Task<List<UserCourseOutput>> GetCourseUserEnroll(string userHashCode);
        Task<List<HomeCourseOutput>> GetCourseCreateByInstructor(string instructorHashCode);
    }
}
