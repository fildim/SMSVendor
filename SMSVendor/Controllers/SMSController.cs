using Microsoft.AspNetCore.Mvc;
using SMSVendor.DTOs;
using SMSVendor.Services;

namespace SMSVendor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SMSController : ControllerBase
    {
        private readonly SMSService _smsService;

        public SMSController(SMSService smsService)
        {
            _smsService = smsService;
        }

        [HttpPost]
        public IActionResult SendSMS(RequestDTO requestDTO)
        {
            try
            {
                _smsService.SendSMS(requestDTO);
                return Ok("SMS sent suceesfully");
            }
            catch (Exception )
            {
                return BadRequest("Try again");
            }
        }

    }
}
