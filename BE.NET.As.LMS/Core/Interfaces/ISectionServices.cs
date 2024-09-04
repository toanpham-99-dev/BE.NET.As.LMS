using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface ISectionServices
    {
        Section GetByHashCode(string hashCode);
        Task<int> CreateSection(SectionInput sectionInput);
        Task<int> UpdateSection(string sectionHashCode, UpdateSectionInput updateSectionInput);
        Task<int> DeleteSection(string sectionHashCode);
        Task<List<SectionOutput>> GetSectionsOrderByPriority(string courseHashCode);
    }
}
