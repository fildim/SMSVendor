using SMSVendor.Models;

namespace SMSVendor.Services
{
    public interface ISMSVendor
    {
        void Send(Message message);
    }

    public class GreekSMSVendor : ISMSVendor
    {
        public void Send(Message message)
        {
            throw new NotImplementedException();
        }
    }

    public class CyprusSMSVendor : ISMSVendor
    {
        public void Send(Message message)
        {
            throw new NotImplementedException();
        }
    }

    public class RestSMSVendor : ISMSVendor
    {
        public void Send(Message message) 
        {
            throw new NotImplementedException();
        }
    }
}
