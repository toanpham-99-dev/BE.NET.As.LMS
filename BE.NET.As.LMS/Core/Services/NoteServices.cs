using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using BE.NET.As.LMS.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class NoteServices : INoteServices
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserServices _userServices;
        public NoteServices(IUnitOfWork uow, IUserServices userServices)
        {
            _uow = uow;
            _userServices = userServices;
        }
        public async Task<Note> GetByHashCode(string hashCode)
        {
            return await _uow.GetRepository<Note>().AsQueryable()
                 .FirstOrDefaultAsync(_ => _.HashCode == hashCode && _.isDeleted == false);
        }
        public async Task<int> AddNote(string userHashCode, string lessonHashCode, NoteInput noteInput)
        {
            var user = await _userServices.GetByHashCode(userHashCode);
            var lesson = _uow.GetRepository<Lesson>().AsQueryable()
                .FirstOrDefault(x => x.HashCode == lessonHashCode);
            if (user == null && lesson == null)
            {
                return -1;
            }
            Note note = await _uow.GetRepository<Note>().AsQueryable()
                 .FirstOrDefaultAsync(_ => _.UserHashCode == userHashCode && _.LessonHashCode == lessonHashCode);
            if (note != null)
            {
                var listNote = Helper.Deserialize<List<NoteInput>>(note.JsonContent);
                listNote.Add(noteInput);
                note.JsonContent = Helper.Serialize(listNote);
                _uow.GetRepository<Note>().Update(note);
                return await _uow.SaveChangesAsync();
            };
            var listNote1 = new List<NoteInput>();
            listNote1.Add(noteInput);
            note = new Note()
            {

                JsonContent = Helper.Serialize<List<NoteInput>>(listNote1),
                LessonId = lesson.Id,
                LessonHashCode = lessonHashCode,
                UserId = user.Id,
                UserHashCode = userHashCode,
            };
            _uow.GetRepository<Note>().Add(note);
            return await _uow.SaveChangesAsync();
        }

        public async Task<int> DeleteNote(string hashCode)
        {
            var note = await GetByHashCode(hashCode);
            if (note == null)
            {
                return -1;
            }
            _uow.GetRepository<Note>().Delete(note);
            return await _uow.SaveChangesAsync();
        }

        public async Task<int> UpdateNote(string hashCode, string userHashCode, string lessonHashCode, NoteInput noteInput)
        {
            var note = await GetByHashCode(hashCode);
            var user = await _userServices.GetByHashCode(userHashCode);
            var lesson = _uow.GetRepository<Course>().AsQueryable()
                .FirstOrDefault(x => x.HashCode == lessonHashCode);
            if (user == null && lesson == null)
            {
                return -1;
            }
            var listNote = Helper.Deserialize<List<NoteInput>>(note.JsonContent);
            foreach (var item in listNote)
            {
                if (item.TimeSpan == noteInput.TimeSpan)
                {
                    item.Content = noteInput.Content;
                }
                if (item.Content == "")
                {
                    listNote.Remove(item);
                    break;
                }
            }
            note.JsonContent = Helper.Serialize(listNote);
            _uow.GetRepository<Note>().Update(note);
            return await _uow.SaveChangesAsync();
        }

        public async Task<ApiResponse<NoteOutput>> GetListNote(string lessonHashCode, string userHashCode)
        {
            var note = await _uow.GetRepository<Note>().AsQueryable()
                .Where(_ => _.LessonHashCode == lessonHashCode && _.UserHashCode == userHashCode && _.isDeleted == false)
                .Select(_ => new NoteOutput
                {
                    HashCode = _.HashCode,
                    NoteJsons = Helper.Deserialize<List<NoteInput>>(_.JsonContent),
                    LessonHashCode = _.LessonHashCode,
                    UserHashCode = _.UserHashCode,

                }).FirstOrDefaultAsync();
            if (note == null)
            {
                return new ApiResponse<NoteOutput>()
                {
                    Data = null,
                    Message = "There are no notes",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            };
            return new ApiResponse<NoteOutput>()
            {
                Data = note,
                Message = "Get Note Successful",
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
