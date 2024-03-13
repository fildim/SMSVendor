using Microsoft.AspNetCore.Mvc;
using SMSVendor.DTOs;
using SMSVendor.Models;

namespace SMSVendor.Services
{
    public interface ISMSService
    {
        Task SendSMS(InnerDTO sms);
    }

    public class SMSService : ISMSService
    {
        private readonly Dictionary<string, ISMSVendor> _vendors;

        public SMSService(ISMSVendor grSMSVendor, ISMSVendor cySMSVendor, ISMSVendor restSMSVendor)
        {
            _vendors = new Dictionary<string, ISMSVendor>
            {
                { "GR", grSMSVendor },
                { "CY", cySMSVendor },
                { "OT", restSMSVendor }
            };
        }

        public async Task SendSMS(InnerDTO sms)
        {
            try
            {
                string countryCode = sms.RecipientNumber.Substring(0, 2);
                ISMSVendor vendor = _vendors.GetValueOrDefault(countryCode, _vendors["Other"]);

                if (sms.SMSText.Length > 480)
                {
                    throw new Exception("Message too big");
                }
                else
                {
                    await vendor.Send(new Message
                    {
                        Recipient = sms.RecipientNumber,
                        Text = sms.SMSText
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
