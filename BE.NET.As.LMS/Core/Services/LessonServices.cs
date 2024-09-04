using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class LessonServices : ILessonServices
    {
        private readonly IUnitOfWork _uow;
        private readonly ISectionServices _sectionServices;
        public LessonServices(IUnitOfWork uow, ISectionServices sectionServices, ICourseServices courseServices)
        {
            _uow = uow;
            _sectionServices = sectionServices;
        }
        public Lesson GetByHashCode(string hashCode)
        {
            return _uow.GetRepository<Lesson>().AsQueryable()
                .FirstOrDefault(x => x.HashCode == hashCode && x.isDeleted == false);
        }
        public async Task<int> CreateLesson(LessonInput lessonInput)
        {
            try
            {
                var section = _sectionServices.GetByHashCode(lessonInput.SectionHashCode);
                if (section == null)
                {
                    return -1;
                }
                {
                    var lesson = new Lesson();
                    lesson.Name = lessonInput.Name;
                    lesson.Description = lessonInput.Description;
                    lesson.LinkVideo = lessonInput.LinkVideo;
                    lesson.Duration = lessonInput.Duration;
                    lesson.SectionId = section.Id;
                    IEnumerable<Lesson> lessons = _uow.GetRepository<Lesson>()
                        .AsQueryable()
                        .Where(x => x.SectionId == section.Id && x.isDeleted == false);
                    int temp = lessonInput.Priority;
                    foreach (var item in lessons)
                    {
                        if (item.Priority == temp)
                        {
                            item.Priority += 1;
                            temp = item.Priority;
                            _uow.GetRepository<Lesson>().Update(item);
                        }
                    }
                    lesson.Priority = lessonInput.Priority;
                    _uow.GetRepository<Lesson>().Add(lesson);
                    return await _uow.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<int> UpdateLesson(string lessonHashCode, UpdateLessonInput updateLessonInput)
        {
            try
            {
                var lesson = GetByHashCode(lessonHashCode);
                var section = _sectionServices.GetByHashCode(updateLessonInput.SectionHashCode);
                if (lesson == null || section == null)
                {
                    return -1;
                }
                lesson.Name = updateLessonInput.Name;
                lesson.Description = updateLessonInput.Description;
                lesson.LinkVideo = updateLessonInput.LinkVideo;
                lesson.Duration = updateLessonInput.Duration;
                lesson.UpdatedAt = DateTime.Now;
                IEnumerable<Lesson> lessons = _uow.GetRepository<Lesson>()
                    .AsQueryable()
                    .Where(x => x.SectionId == section.Id && x.isDeleted == false);
                int temp = updateLessonInput.Priority;
                foreach (var item in lessons)
                {
                    if (item.Priority == temp)
                    {
                        item.Priority += 1;
                        temp = item.Priority;
                        _uow.GetRepository<Lesson>().Update(item);
                    }
                }
                lesson.Priority = updateLessonInput.Priority;
                _uow.GetRepository<Lesson>().Update(lesson);
                return await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<int> DeleteLesson(string lessonHashCode)
        {
            try
            {
                var lesson = await _uow.GetRepository<Lesson>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.HashCode == lessonHashCode && x.isDeleted == false);
                if (lesson == null)
                {
                    return -1;
                }
                lesson.isDeleted = true;
                _uow.GetRepository<Lesson>().Update(lesson);
                return await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<List<LessonOutput>> GetLessonsOrderByPriority(string sectionHashCode)
        {
            return await _uow.GetRepository<Lesson>()
                .AsQueryable()
                .Include(x => x.Section)
                .Where(x => x.Section.HashCode == sectionHashCode && x.isDeleted == false)
                .OrderBy(x => x.Priority)
                .Select(x => new LessonOutput
                {
                    Name = x.Name,
                    Duration = x.Duration,
                    Description = x.Description,
                    LinkVideo = x.LinkVideo,
                })
                .ToListAsync();
        }
    }
}
