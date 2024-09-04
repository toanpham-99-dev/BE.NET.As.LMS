using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IAdminServices
    {
        Task<User> GetUserByHashCode(string hashCode);
        Task<List<UserDetailOutput>> GetListUser();
        Task<PageResponse<UserDetailOutput>> GetUserPaging(string roleHashCode, PagingInput pagingInput);
        Task<ApiResponse<UserDetailOutput>> GetUserDetail(string hashCode);
        Task<int> AddUser(RegisterInput registerInput, string roleHashCode);
        Task<bool> UpdateDetailUser(UserUpdateInput userUpdateInput, string hashCode);
        Task<bool> UpdateUserInLine(UserUpdateInput userUpdateInput, string hashCode);
        Task<int> DeleteUser(string hashCode);
        Task<int> RecoverUser(string hashCode);
        Task<StatisticOutput> GetStatistics();
        Task<List<SearchInstructorOutput>> SearchByInstructor(string searchString, int pageSize, int pageIndex);
    }
}
