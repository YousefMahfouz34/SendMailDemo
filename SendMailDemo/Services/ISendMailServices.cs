namespace SendMailDemo.Services
{
    public interface ISendMailServices
    {
        Task SendMail(string mailto, string subject, string body);
    }
}
