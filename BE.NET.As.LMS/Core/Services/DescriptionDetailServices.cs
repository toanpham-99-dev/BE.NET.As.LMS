using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Output;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class DescriptionDetailServices : IDescriptionDetailServices
    {
        private readonly IUnitOfWork _uow;
        private IUserServices _userServices;
        private ICourseServices _courseServices;
        public DescriptionDetailServices(IUnitOfWork uow, IUserServices userServices,
            ICourseServices courseServices)
        {
            _uow = uow;
            _userServices = userServices;
            _courseServices = courseServices;
        }
        public async Task<int> AddDescriptionDetail(string courseHashCode, string description, string userHashCode)
        {
            User user = await _userServices.GetByHashCode(userHashCode);
            Course course = await _courseServices.GetByHashCode(courseHashCode);
            if (course == null || user == null)
                return -1;
            try
            {
                _uow.BeginTransaction();
                DescriptionDetail descriptionDetail = new DescriptionDetail();
                descriptionDetail.Description = description;
                descriptionDetail.CourseId = course.Id;
                descriptionDetail.CreatedAt = DateTime.Now;
                _uow.GetRepository<DescriptionDetail>().Add(descriptionDetail);
                await _uow.SaveChangesAsync();

                course.DescriptionDetailId = descriptionDetail.Id;
                course.UpdatedBy = user.UserName;
                course.UpdatedAt = DateTime.Now;
                _uow.GetRepository<Course>().Update(course);
                await _uow.SaveChangesAsync();
                _uow.CommitTransaction();
                return 1;
            }
            catch
            {
                _uow.RollbackTransaction();
                return -1;
            }
        }

        public async Task<DescriptionDetailOutput> GetByCourse(string courseHashCode)
        {
            Course course = await _uow.GetRepository<Course>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(_ => _.isDeleted == false &&
                                         _.HashCode == courseHashCode);
            if (course == null)
                return null;
            return await _uow.GetRepository<DescriptionDetail>()
                .AsQueryable()
                .Where(_ => _.isDeleted == false &&
                       _.Course.HashCode == courseHashCode)
                .Select(_ => new DescriptionDetailOutput
                {
                    HashCode = _.HashCode,
                    Description = _.Description,
                    CourseHashCode = courseHashCode,
                    CourseName = course.Title,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateDescriptionDetail(string hashCode, string description)
        {
            DescriptionDetail descriptionDetail = await _uow.GetRepository<DescriptionDetail>()
                .AsQueryable()
                .FirstOrDefaultAsync(_ => _.isDeleted == false &&
                                     _.HashCode == hashCode);
            if (descriptionDetail == null)
                return - 1;
            try
            {
                descriptionDetail.Description = description;
                descriptionDetail.UpdatedAt = DateTime.Now;
                _uow.GetRepository<DescriptionDetail>().Update(descriptionDetail);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }
    }
}
