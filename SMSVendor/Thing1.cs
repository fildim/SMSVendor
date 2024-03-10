using SMSVendor.Services;

namespace SMSVendor
{
    public class Thing1
    {
    }

    public interface ISMSSender { }

    public class SmsSender1 : ISMSSender { }

    public class RestSmsSender : ISMSSender { }

    public interface ISmsSenderFactory
    {
        ISMSSender Build(string countryCode);
    }

    public class SmsSenderFactory : ISmsSenderFactory
    {
        private readonly IReadOnlyDictionary<string, ISMSSender> smsServices;

        public SmsSenderFactory(IReadOnlyDictionary<string, ISMSSender> smsServices)
        {
            this.smsServices = smsServices;
        }

        public ISMSSender Build(string countryCode)
        {
            return this.smsServices.SingleOrDefault(_ => _.Key == countryCode).Value ?? new RestSmsSender();
        }
    }

    public class Soemth
    {
        private readonly ISmsSenderFactory smsSenderFactory;

        public Soemth(ISmsSenderFactory smsSenderFactory)
        {
            this.smsSenderFactory = smsSenderFactory;
        }

        public Task X(string recipientPhoneNumber)
        {
            return (Task)this.smsSenderFactory.Build(recipientPhoneNumber.Substring(0, 4));
        }
    }
}
