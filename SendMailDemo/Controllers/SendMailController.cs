using Microsoft.AspNetCore.Mvc;
using SendMailDemo.Services;

namespace SendMailDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendMailController : ControllerBase
    {
        private readonly ISendMailServices sendMailServices;

        public SendMailController(ISendMailServices sendMailServices)
        {
            this.sendMailServices = sendMailServices;
        }
        [HttpPost]
        public  async Task<IActionResult> SendMail(String mailto, String subject, String body)
        {
            try
            {
                await sendMailServices.SendMail(mailto, subject, body);
                return Ok("sent");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

    }
}
