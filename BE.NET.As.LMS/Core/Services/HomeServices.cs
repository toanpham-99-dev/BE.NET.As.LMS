using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Output;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class HomeServices : IHomeServices
    {
        private readonly IUnitOfWork _uow;
        public HomeServices(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<List<HomeCourseOutput>> GetCourseOutstanding(int take)
        {
            return await _uow.GetRepository<Course>().AsQueryable()
                .Where(_ => _.isDeleted == false && _.Status == 1)
                .OrderByDescending(_ => _.ViewCount)
                .Select(_ => new HomeCourseOutput
                {
                    HashCode = _.HashCode,
                    Price = _.Price,
                    Content = _.Content,
                    Title = _.Title,
                    ViewCount = _.ViewCount,
                    ImageURL = _.ImageURL,
                    Summary = _.Summary,
                    Duration = _.Duration
                })
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<HomeCourseOutput>> GetCourseNew(int take)
        {
            return await _uow.GetRepository<Course>().AsQueryable()
                .Where(_ => _.isDeleted == false && _.Status == 1)
                .OrderByDescending(_ => _.Id)
                .Select(_ => new HomeCourseOutput
                {
                    HashCode = _.HashCode,
                    Price = _.Price,
                    Content = _.Content,
                    Title = _.Title,
                    ViewCount = _.ViewCount,
                    ImageURL = _.ImageURL,
                    Summary = _.Summary,
                    Duration = _.Duration
                })
                .Take(take)
                .ToListAsync();
        }

    }
}
