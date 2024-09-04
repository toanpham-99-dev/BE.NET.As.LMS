using BE.NET.As.LMS.DTOs.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IHomeServices
    {
        Task<List<HomeCourseOutput>> GetCourseOutstanding(int take);
        Task<List<HomeCourseOutput>> GetCourseNew(int take);
    }
}
