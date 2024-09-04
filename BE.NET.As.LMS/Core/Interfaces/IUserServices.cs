using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IUserServices
    {
        Task<User> GetByHashCode(string hashCode);
        Task<UserOutput> GetDetailByHashCode(string hashCode);
        Task<bool> UpdateUser(UserInput userInput, string hashCode);
        Task<List<UserNotificationOutput>> NotificationUser(string hashCode);
        Task<bool> UserEnroll(string userHashCode, string courseHashCode);
        Task<List<SectionOutput>> GetSectionsByUserCourse(string courseHashCode, string userHashCode);
    }
}
