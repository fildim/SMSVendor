using SMSVendor.Models;
using SMSVendor.Repositories;
using System.IO;

namespace SMSVendor.Services
{
    public interface ISMSVendor
    {
        void Send(Message message);
    }

    public class GreekSMSVendor : ISMSVendor
    {
        private readonly IMessageRepository _messageRepository;

        public GreekSMSVendor(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public void Send(Message message)
        {
            if (ContainsOnlyGreekCharacters(message.Text))
            {
                _messageRepository.Send(message);
            }
            else
            {
                throw new Exception("Can only send greek messages");
            }
        }


        private bool ContainsOnlyGreekCharacters(string text)
        {
            foreach (char c in text)
            {
                if ((c >= '\u0391' && c <= '\u03A9') || // Greek capital letters
                    (c >= '\u03B1' && c <= '\u03C9'))   // Greek small letters
                {
                    return true;
                }
            }
            return false;
        }
    }



    public class CyprusSMSVendor : ISMSVendor
    {
        private readonly IMessageRepository _messageRepository;

        public CyprusSMSVendor(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public void Send(Message message)
        {
            if (message.Text.Length <= 160)
            {
                _messageRepository.Send(message);
            }
            else
            {
                var messageParts = splitMessage(message.Text);

                foreach (string part in messageParts)
                {
                    // Send each part as a separate SMS
                    _messageRepository.Send(new Message { Recipient = message.Recipient, Text = part });
                }
            }
            
        }

        private List<string> splitMessage(string text)
        {
            var result = new List<string>();

            for (int i = 0; i < text.Length; i += 160)
            {
                int length = Math.Min(160, text.Length - i);
                result.Add(text.Substring(i, length));
            }

            return result;
        }
    }



    public class RestSMSVendor : ISMSVendor
    {
        private readonly IMessageRepository _messageRepository;

        public RestSMSVendor(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public void Send(Message message) 
        {
            _messageRepository.Send(message);
        }
    }

    
}
