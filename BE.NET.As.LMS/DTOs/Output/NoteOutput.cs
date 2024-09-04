using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output.Base;
using System.Collections.Generic;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class NoteOutput : BaseOutput
    {
        public List<NoteInput> NoteJsons { get; set; }
        public string LessonHashCode { get; set; }
        public string UserHashCode { get; set; }
    }
}
