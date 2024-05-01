namespace SendMailDemo.Services
{
    public interface ISendMailServices
    {
        Task<bool> SendMail( BodyDto body);
    }
}
