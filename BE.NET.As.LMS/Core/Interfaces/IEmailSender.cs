using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string subject, string email, string content);
    }
}
