using System.Threading.Tasks;

namespace Escolaridad.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
