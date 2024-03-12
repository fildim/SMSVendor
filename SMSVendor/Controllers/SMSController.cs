using Microsoft.AspNetCore.Mvc;
using SMSVendor.DTOs;
using SMSVendor.Mappers;
using SMSVendor.Models;
using SMSVendor.Services;
using AutoMapper;

namespace SMSVendor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SMSController : ControllerBase
    {
        private readonly ISMSService _smsService;
        private readonly SMSRequestMapper _smsRequestMapper;

        public SMSController(SMSService smsService, SMSRequestMapper smsRequestMapper)
        {
            _smsService = smsService;
            _smsRequestMapper = smsRequestMapper;
        }

        [HttpPost]
        public IActionResult SendSMS(RequestDTO requestDTO)
        {
            var sms = _smsRequestMapper.Map<InnerDTO>(requestDTO);

            try
            {
                _smsService.SendSMS(sms);
                return Ok("SMS sent succesfully");
            }
            catch (Exception )
            {
                throw;
                //return BadRequest("Try again");
            }
        }

    }
}
