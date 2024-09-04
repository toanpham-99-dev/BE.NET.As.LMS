using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface INoteServices
    {
        Task<Note> GetByHashCode(string hashCode);
        Task<int> AddNote(string userHashCode, string lessonHashCode, NoteInput noteInput);
        Task<ApiResponse<NoteOutput>> GetListNote(string lessonHashCode, string userHashCode);
        Task<int> UpdateNote(string hashCode, string userHashCode, string lessonHashCode, NoteInput noteInput);
        Task<int> DeleteNote(string hashCode);
    }
}
