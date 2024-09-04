using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IAssignmentServices
    {
        Assignment GetByHashCode(string hashcode);
        Task<List<AssignmentOutput>> GetAll();
        Task<List<AssignmentOutput>> GetAllByLesson(string hashCode);
        Task<List<AssignmentUserOutput>> GetAllByUser(string hashCode);
        Task<List<AssignmentUserOutput>> GetAllCurrentByUser(long currentUserId);
        Task<int> AddAssignmentUser(long currentUserId, AssignmentUserInput assignmentUserInput);
        Task<int> UpdateAssignmentUser(long currentUserId, string hashCode, AssignmentUserInput assignmentUserInput);
        Task<int> AddAssignment(AssignmentInput assignmentInput);
        Task<int> UpdateAssignment(string hashCode, AssignmentInput assignmentInput);
        Task<int> DeleteAssignment(string hashCode);
    }
}
