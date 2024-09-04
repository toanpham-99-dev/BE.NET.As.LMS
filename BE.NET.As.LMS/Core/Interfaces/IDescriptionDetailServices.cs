using BE.NET.As.LMS.DTOs.Output;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IDescriptionDetailServices
    {
        Task<DescriptionDetailOutput> GetByCourse(string courseHashCode);
        Task<int> AddDescriptionDetail(string courseHashCode, string description, string userHashCode);
        Task<int> UpdateDescriptionDetail(string hashCode, string description);
    }
}
