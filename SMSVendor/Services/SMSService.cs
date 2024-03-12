using SMSVendor.DTOs;
using SMSVendor.Models;

namespace SMSVendor.Services
{
    public interface ISMSService
    {
        void SendSMS(InnerDTO sms);
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
                { "Other", restSMSVendor }
            };
        }

        public void SendSMS(InnerDTO sms)
        {
            try
            {
                string countryCode = sms.RecipientNumber.Substring(0, 4);
                ISMSVendor vendor = _vendors.GetValueOrDefault(countryCode, _vendors["Other"]);

                if (sms.SMSText.Length > 480)
                {
                    throw new Exception("Message too big");
                }
                else
                {
                    vendor.Send(new Message
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
