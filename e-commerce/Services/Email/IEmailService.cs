namespace e_commerce.Services.Email
{
    public interface IEmailService
    {
        Task SendAsync(string toEmail, string subject, string htmlBody);

    }
}
