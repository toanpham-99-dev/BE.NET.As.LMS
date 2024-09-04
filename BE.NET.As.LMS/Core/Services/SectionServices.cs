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
    public class SectionServices : ISectionServices
    {
        private readonly IUnitOfWork _uow;
        private readonly ICourseServices _courseServices;
        public SectionServices(IUnitOfWork unitOfWork, ICourseServices courseServices)
        {
            _uow = unitOfWork;
            _courseServices = courseServices;
        }
        public Section GetByHashCode(string hashCode)
        {
            return _uow.GetRepository<Section>().AsQueryable()
                .FirstOrDefault(x => x.HashCode == hashCode);
        }
        public async Task<int> CreateSection(SectionInput sectionInput)
        {
            try
            {
                var course = await _courseServices.GetByHashCode(sectionInput.CourseHashCode);
                if (course == null)
                {
                    return -1;
                }
                var section = new Section();
                section.Description = sectionInput.Description;
                section.CourseId = course.Id;
                IEnumerable<Section> sections = _uow.GetRepository<Section>()
                    .AsQueryable()
                    .Where(x => x.CourseId == course.Id);
                int temp = sectionInput.Priority;
                foreach (var item in sections)
                {
                    if (item.Priority == temp)
                    {
                        item.Priority += 1;
                        temp = item.Priority;
                        _uow.GetRepository<Section>().Update(item);
                    }
                }
                section.Priority = sectionInput.Priority;
                _uow.GetRepository<Section>().Add(section);
                return await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<int> UpdateSection(string sectionHashCode, UpdateSectionInput updateSectionInput)
        {
            try
            {
                var section = GetByHashCode(sectionHashCode);
                var course = await _courseServices.GetByHashCode(updateSectionInput.CourseHashCode);
                if (section == null || course == null)
                {
                    return -1;
                }
                section.Description = updateSectionInput.Description;
                section.CourseId = course.Id;
                section.Status = updateSectionInput.Status;
                section.UpdatedAt = DateTime.Now;
                IEnumerable<Section> sections = _uow.GetRepository<Section>()
                    .AsQueryable()
                    .Where(x => x.CourseId == course.Id && x.isDeleted == false);
                int temp = updateSectionInput.Priority;
                foreach (var item in sections)
                {
                    if (item.Priority == temp)
                    {
                        item.Priority += 1;
                        temp = item.Priority;
                        _uow.GetRepository<Section>().Update(item);
                    }
                }
                section.Priority = updateSectionInput.Priority;
                _uow.GetRepository<Section>().Update(section);
                return await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<int> DeleteSection(string sectionHashCode)
        {
            try
            {
                var section = await _uow.GetRepository<Section>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.HashCode == sectionHashCode && x.isDeleted == false);
                if (section == null)
                {
                    return -1;
                }
                section.isDeleted = true;
                _uow.GetRepository<Section>().Update(section);
                return await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<List<SectionOutput>> GetSectionsOrderByPriority(string courseHashCode)
        {
            return await _uow.GetRepository<Section>()
                .AsQueryable()
                .Include(x => x.Course)
                .Where(x => x.Course.HashCode == courseHashCode && x.isDeleted == false)
                .OrderBy(x => x.Priority)
                .Select(x => new SectionOutput
                {
                    Description = x.Description,
                })
                .ToListAsync();
        }
    }
}
