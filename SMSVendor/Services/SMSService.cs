using SMSVendor.DTOs;
using SMSVendor.Models;

namespace SMSVendor.Services
{
    

    public class SMSService
    {
        private readonly Dictionary<string, ISMSVendor> _vendors;

        public SMSService(ISMSVendor grSMSVendor, ISMSVendor cySMSVendor, ISMSVendor restSMSVendor)
        {
            _vendors = new Dictionary<string, ISMSVendor>
            {
                { "GR", grSMSVendor },
                { "CY", cySMSVendor },
                { "Other", restSMSVendor }
            };
        }

        public void SendSMS(RequestDTO requestDTO)
        {
            try
            {
                string countryCode = requestDTO.Number.Substring(0, 4);
                ISMSVendor vendor = _vendors.GetValueOrDefault(countryCode, _vendors["Other"]);

                if (requestDTO.Message.Length > 480)
                {
                    throw new Exception("Message too big");
                }
                else
                {
                    vendor.Send(new Message
                    {
                        Recipient = requestDTO.Number,
                        Text = requestDTO.Message,
                        Created = DateTime.UtcNow
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
