using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using System;
using BE.NET.As.LMS.DTOs.Output;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static BE.NET.As.LMS.Utilities.Constaint;
using BE.NET.As.LMS.Utilities;

namespace BE.NET.As.LMS.Core.Services
{
    public class CourseServices : ICourseServices
    {
        private readonly IUnitOfWork _uow;
        private readonly ICategoryServices _categoryServices;
        private readonly IQuizServices _quizServices;
        private readonly IAssignmentServices _assignmentServices;
        private readonly IFileStorageServices _fileStorageServices;
        private readonly INoteServices _noteServices;
        private readonly ICommentServices _commentServices;
        public CourseServices(IUnitOfWork uow, ICategoryServices categoryServices,
            IFileStorageServices fileStorageServices,
            IQuizServices quizServices, IAssignmentServices assignmentServices,
            INoteServices noteServices, ICommentServices commentServices)
        {
            _categoryServices = categoryServices;
            _fileStorageServices = fileStorageServices;
            _uow = uow;
            _quizServices = quizServices;
            _assignmentServices = assignmentServices;
            _noteServices = noteServices;
            _commentServices = commentServices;
        }

        public async Task<Course> GetByHashCode(string hashcode)
        {
            return await _uow.GetRepository<Course>().AsQueryable()
                .FirstOrDefaultAsync(_ => _.HashCode == hashcode && _.isDeleted == false);
        }

        public async Task<List<SearchCourseOutput>> SearchByName(string searchName)
        {
            IEnumerable<Course> courses = await _uow.GetRepository<Course>()
                .AsQueryable()
                .Where(_ => _.isDeleted == false)
                .ToListAsync();
            if (!String.IsNullOrEmpty(searchName))
                courses = courses.Where(_ => _.Title.ToLower().Contains(searchName));

            return courses.Select(_ => new SearchCourseOutput()
            {
                Title = _.Title,
                ImageURL = _.ImageURL,
            }).ToList();
        }
        public async Task<int> CreateCourse(CourseInput courseInput, string userHashCode)
        {
            try
            {
                User user = await _uow.GetRepository<User>().AsQueryable()
                   .Where(x => x.HashCode == userHashCode && x.isDeleted == false)
                   .FirstOrDefaultAsync();
                var category = await _categoryServices.GetByHashCode(courseInput.CategoryHashCode);
                if (category == null || user == null)
                {
                    return -1;
                }
                var course = new Course()
                {
                    Title = courseInput.Title,
                    Summary = courseInput.Summary,
                    Syllabus = courseInput.Syllabus,
                    Content = courseInput.Content,
                    Description = courseInput.Description,
                    Level = courseInput.Level,
                    Price = courseInput.Price,
                    ViewCount = 0,
                    Category = category,
                    CreatedBy = user.FullName,
                    InstructorHashCodeCreated = user.HashCode,
                };
                if (courseInput.ImageURL != null)
                {
                    var newImageURL = await _fileStorageServices.SaveFileAsync(courseInput.ImageURL);
                    course.ImageURL = newImageURL;
                }
                _uow.GetRepository<Course>().Add(course);
                return await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<int> UpdateCourse(string courseHashCode, UpdateCourseInput updateCourseInput, string userHashCode)
        {
            try
            {
                User user = await _uow.GetRepository<User>().AsQueryable()
                   .Where(x => x.HashCode == userHashCode && x.isDeleted == false)
                   .FirstOrDefaultAsync();
                var course = await GetByHashCode(courseHashCode);
                var category = await _categoryServices.GetByHashCode(updateCourseInput.CategoryHashCode);
                if (course == null || category == null || user == null)
                {
                    return -1;
                }
                course.Title = updateCourseInput.Title;
                course.Summary = updateCourseInput.Summary;
                if (updateCourseInput.ImageURL != null)
                {
                    var newImageURL = await _fileStorageServices.SaveFileAsync(updateCourseInput.ImageURL);
                    course.ImageURL = newImageURL;
                }
                course.Syllabus = updateCourseInput.Syllabus;
                course.Content = updateCourseInput.Content;
                course.Description = updateCourseInput.Description;
                course.Level = updateCourseInput.Level;
                course.Price = updateCourseInput.Price;
                course.Status = updateCourseInput.Status;
                course.CategoryId = category.Id;
                course.UpdatedBy = user.FullName;
                course.UpdatedAt = DateTime.Now;
                _uow.GetRepository<Course>().Update(course);
                return await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<int> DeleteCourse(string courseHashCode, string userHashCode)
        {
            try
            {
                User user = await _uow.GetRepository<User>().AsQueryable()
                   .Where(x => x.HashCode == userHashCode && x.isDeleted == false)
                   .FirstOrDefaultAsync();
                var course = await _uow.GetRepository<Course>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.HashCode == courseHashCode && x.isDeleted == false);
                if (course == null || user == null)
                {
                    return -1;
                }
                course.isDeleted = true;
                _uow.GetRepository<Course>().Update(course);
                return await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<GuestCourseDetailOutput> GuestCourseDetail(string hashCode)
        {
            return await _uow.GetRepository<Course>().AsQueryable()
                .Where(_ => _.HashCode == hashCode && _.Status == 1 && _.isDeleted == false)
                .Include(_ => _.Category)
                .Include(_ => _.Sections)
                .ThenInclude(_ => _.Lessons)
                .Select(_ => new GuestCourseDetailOutput
                {
                    HashCode = _.HashCode,
                    Summary = _.Summary,
                    ImageURL = _.ImageURL,
                    Rating = _.Rating,
                    Title = _.Title,
                    Content = _.Content,
                    CourseDuration = _.Duration,
                    Level = _.Level,
                    Syllabus = _.Syllabus,
                    Price = _.Price,
                    ViewCount = _.ViewCount,
                    CategoryName = _.Category.Title,
                    Section = _.Sections
                    .Where(_ => _.isDeleted == false)
                    .Select(_ => new SectionOutput
                    {
                        HashCode = _.HashCode,
                        Description = _.Description,
                        Lesson = _.Lessons
                        .Where(_ => _.isDeleted == false)
                        .Select(_ => new LessonOutput
                        {
                            HashCode = _.HashCode,
                            Duration = _.Duration,
                            Name = _.Name,
                        }).ToList(),
                    }).ToList(),
                }).FirstOrDefaultAsync();
        }

        public async Task<SyllabusOfCourseOutput> GetSyllabusOfCourse(string hashCode)
        {
            return await _uow.GetRepository<Course>().AsQueryable()
                .Where(_ => _.HashCode == hashCode && _.Status == 1 && _.isDeleted == false)
                .Select(_ => new SyllabusOfCourseOutput
                {
                    Syllabus = _.Syllabus
                }).FirstOrDefaultAsync();
        }

        public async Task<RatingOfCourseOutput> GetRatingOfCourse(string hashCode)
        {
            return await _uow.GetRepository<Course>().AsQueryable()
                .Where(_ => _.HashCode == hashCode && _.Status == 1 && _.isDeleted == false)
                .Select(_ => new RatingOfCourseOutput
                {
                    Rating = _.Rating
                }).FirstOrDefaultAsync();
        }

        public async Task<List<SearchCourseOutput>> SearchByPrice(decimal searchPrice, int pageSize, int pageIndex)
        {
            IEnumerable<Course> courses = await _uow.GetRepository<Course>()
                .AsQueryable()
                .Where(_ => _.isDeleted == false)
                .ToListAsync();
            if (!searchPrice.Equals(null))
                courses = courses.Where(_ => _.Price == searchPrice)
                                 .OrderBy(_ => _.Title);
            courses = Helper.Paging<Course>(courses.ToList(), pageSize, pageIndex);
            return courses.Select(_ => new SearchCourseOutput()
            {
                Title = _.Title,
                ImageURL = _.ImageURL
            }).ToList();
        }

        public async Task<List<SearchCourseOutput>> SearchByLevel(EnumCourseLevel searchLevel, int pageSize, int pageIndex)
        {
            IEnumerable<Course> courses = await _uow.GetRepository<Course>()
                .AsQueryable()
                .Where(_ => _.isDeleted == false)
                .ToListAsync();
            if (!searchLevel.Equals(null))
                courses = courses.Where(_ => _.Level == searchLevel)
                                 .OrderBy(_ => _.Title);
            courses = Helper.Paging<Course>(courses.ToList(), pageSize, pageIndex);
            return courses.Select(_ => new SearchCourseOutput()
            {
                Title = _.Title,
                ImageURL = _.ImageURL
            }).ToList();
        }

        public async Task<List<SearchCourseOutput>> SearchByInstructor(string searchString, int pageSize, int pageIndex)
        {
            IEnumerable<UserCourse> usercourses = await _uow.GetRepository<UserCourse>()
                .AsQueryable()
                .Include(u => u.User)
                .Include(c => c.Course)
                .Where(_ => _.User.isDeleted == false)
                .ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
                usercourses = usercourses.Where(_ => _.Course.CreatedBy.Contains(searchString))
                                         .OrderBy(_ => _.Course.Title);
            usercourses = Helper.Paging<UserCourse>(usercourses.ToList(), pageSize, pageIndex);
            return usercourses.Select(_ => new SearchCourseOutput()
            {
                Title = _.Course.Title,
                ImageURL = _.Course.ImageURL
            }).ToList();
        }
        public async Task<List<SearchCategoryOutput>> SearchByCategory(string searchString, int pageSize, int pageIndex)
        {
            IEnumerable<Course> courses = await _uow.GetRepository<Course>()
                .AsQueryable()
                .Include(c => c.Category)
                .Where(_ => _.isDeleted == false)
                .ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
                courses = courses.Where(_ => _.Category.Title.Contains(searchString))
                                       .OrderBy(_ => _.Title);
            courses = Helper.Paging<Course>(courses.ToList(), pageSize, pageIndex);
            return courses.Select(_ => new SearchCategoryOutput()
            {
                Title = _.Title,
                ImageURL = _.ImageURL,
                SubCategories = _.Category.SubCategories
            }).ToList();
        }

        public async Task<List<CourseByCategoryOutput>> GetCoursesByCategory(string hashCode)
        {
            Category category = await _categoryServices.GetByHashCode(hashCode);
            if (category == null)
                return null;
            return await _uow.GetRepository<Course>().AsQueryable()
                .Where(_ => _.isDeleted == false &&
                       _.Status == 1 &&
                       _.Category.HashCode == category.HashCode)
                .Select(_ => new CourseByCategoryOutput
                {
                    HashCode = _.HashCode,
                    Title=_.Title,
                    ImageURL = _.ImageURL,
                    Rating = _.Rating,
                    Duration = _.Duration,
                    Level = _.Level,
                    Price = _.Price,
                    ViewCount = _.ViewCount,
                    CreatedBy = _.CreatedBy,
                    NumberOfStudent = _.UserCourses.Count,
                })
                .ToListAsync();
        }
        private async Task<List<CourseAdminOutput>> GetListCourse()
        {
            return await _uow.GetRepository<Course>()
                .AsQueryable()
                .Where(_ => _.isDeleted == false)
                .OrderBy(_ => _.Title)
                .Select(_ => new CourseAdminOutput()
                {
                    HashCode = _.HashCode,
                    Summary = _.Summary,
                    Title = _.Title,
                    Content = _.Content,
                    ImageURL = _.ImageURL,
                    Description = _.Description,
                    Rating = _.Rating,
                    Duration = _.Duration,
                    Level = _.Level,
                    Syllabus = _.Syllabus,
                    Price = _.Price,
                    ViewCount = _.ViewCount
                })
                .ToListAsync();
        }
        public async Task<int> PublicOrHideCourse(string courseHashCode, CourseStatus courseStatus)
        {
            Course course = await GetByHashCode(courseHashCode);
            if (course == null)
                return -1;
            try
            {
                course.Status = (int)courseStatus;
                course.UpdatedAt = DateTime.Now;
                _uow.GetRepository<Course>().Update(course);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }
        public async Task<List<CourseAdminOutput>> GetAllPagingFilterSearchCourse(string searchString, EnumCourseLevel level, int pageSize, int pageIndex)
        {
            IEnumerable<CourseAdminOutput> courses = await GetListCourse();
            if (!level.Equals(null))
                courses = courses.Where(_ => _.Level == level);
            if (!String.IsNullOrEmpty(searchString))
                courses = courses.Where(_ => _.Title.Contains(searchString)
                    || _.Category.Title.Contains(searchString)
                    || _.CreatedBy.Contains(searchString));
            courses = Helper.Paging<CourseAdminOutput>(courses.ToList(), pageSize, pageIndex);
            return (List<CourseAdminOutput>)(courses = courses.OrderBy(t => t.Title).ToList());
        }

        public async Task<LessonDetailOutput> LessonDetail(string courseHashCode, string userHashCode, string lessonHashCode)
        {
            var userCourse = await _uow.GetRepository<UserCourse>().AsQueryable()
                .FirstOrDefaultAsync(_ => _.UserHashCode == userHashCode && _.CourseHashCode == courseHashCode);
            if (userCourse == null)
            {
                return null;
            }

            var quizs = await _quizServices.GetAllByLesson(lessonHashCode);
            var assignments = await _assignmentServices.GetAllByLesson(lessonHashCode);
            var notes = await _noteServices.GetListNote(lessonHashCode, userHashCode);
            var comments = await _commentServices.GetParentByLessonHashCode(lessonHashCode);
            var courseDetail = await UserCourseDetail(courseHashCode);
            var lessons = await _uow.GetRepository<Lesson>().AsQueryable().FirstOrDefaultAsync(_ => _.HashCode == lessonHashCode);

            _uow.GetRepository<UserCourse>().Update(userCourse);
            await _uow.SaveChangesAsync();

            var totalLesson = _uow.GetRepository<Lesson>()
                .AsQueryable()
                .Where(_ => _.isDeleted == false && _.Section.Course.HashCode == courseHashCode)
                .Count();

            return new LessonDetailOutput()
            {
                Lesson = new LessonOutput()
                {
                    HashCode = lessons.HashCode,
                    Name = lessons.Name,
                    Description = lessons.Description,
                    LinkVideo = lessons.LinkVideo,
                },
                TotalLesson = totalLesson,
                Quiz = quizs,
                Assignments = assignments,
                Notes = notes,
                Comments = comments
            };
        }

        public async Task<UserCourseDetailOutput> UserCourseDetail(string courseHashCode)
        {
            return await _uow.GetRepository<Course>().AsQueryable()
                .Where(_ => _.HashCode == courseHashCode && _.Status == 1 && _.isDeleted == false)
                .Include(_ => _.Category)
                .Include(_ => _.Sections)
                .ThenInclude(_ => _.Lessons)
                .Select(_ => new UserCourseDetailOutput()
                {
                    HashCode = _.HashCode,
                    Summary = _.Summary,
                    ImageURL = _.ImageURL,
                    Rating = _.Rating,
                    Title = _.Title,
                    Content = _.Content,
                    CourseDuration = _.Duration,
                    Level = _.Level,
                    Syllabus = _.Syllabus,
                    Price = _.Price,
                    ViewCount = _.ViewCount,
                    CategoryName = _.Category.Title,
                    Sections = _.Sections
                        .Where(_ => _.isDeleted == false)
                        .Select(_ => new SectionOutput()
                        {
                            HashCode = _.HashCode,
                            Description = _.Description,
                            Lesson = _.Lessons
                                .Where(_ => _.isDeleted == false)
                                .Select(_ => new LessonOutput
                                {
                                    HashCode = _.HashCode,
                                    Duration = _.Duration,
                                    Name = _.Name,
                                    LinkVideo = _.LinkVideo,
                                    Description = _.Description
                                }).ToList(),
                        }).ToList(),
                }).FirstOrDefaultAsync();
        }

        public async Task<List<UserCourseOutput>> GetCourseUserEnroll(string userHashCode)
        {
            var userCourse = await _uow.GetRepository<UserCourse>()
                .AsQueryable()
                .Where(_=>_.UserHashCode == userHashCode)
                .Include(_ => _.Course)
                .Where(_ => _.UserHashCode == userHashCode)
                .Select(_ => new UserCourseOutput()
                {
                    HashCode = _.HashCode,
                    Price = _.Course.Price,
                    Content = _.Course.Content,
                    Title = _.Course.Title,
                    ViewCount = _.Course.ViewCount,
                    ImageURL = _.Course.ImageURL,
                    Summary = _.Course.Summary,
                    Duration = _.Course.Duration,
                    TotalLesson = _.TotalLesson,
                    Completed = _.Completed
                }).ToListAsync();
            if (userCourse == null)
            {
                return null;
            }
            return userCourse;
        }

        public async Task<List<HomeCourseOutput>> GetCourseCreateByInstructor(string instructorHashCode)
        {
            var instructorCourse = await _uow.GetRepository<Course>()
                .AsQueryable()
                .Where(_ => _.InstructorHashCodeCreated == instructorHashCode)
                .Select(_ => new HomeCourseOutput()
                {
                    HashCode = _.HashCode,
                    Price = _.Price,
                    Content = _.Content,
                    Title = _.Title,
                    ViewCount = _.ViewCount,
                    ImageURL = _.ImageURL,
                    Summary = _.Summary,
                    CreatedBy = _.CreatedBy,
                    Duration = _.Duration
                }).ToListAsync();
            if (instructorCourse == null)
            {
                return null;
            }
            return instructorCourse;
        }
    }
}
