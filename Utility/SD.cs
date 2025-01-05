using Microsoft.AspNetCore.Identity.UI.Services;
namespace WebApplication2.Utility
{
    public static class SD
    {
        public const string Role_admin = "Admin";
        public const string Role_fournisseur = "Fournisseur";
        public const string Role_client = "Client";
    }
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }

}
