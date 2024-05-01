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
        public  async Task<IActionResult> SendMail( BodyDto body)
        {
            Response response = new Response();
            try
            {
                await sendMailServices.SendMail( body);
                response.message = "Email Send";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = "Email not Send";
                return BadRequest(response);

            }

        }

    }
}
