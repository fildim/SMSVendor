using SMSVendor.Models;

namespace SMSVendor.Repositories
{
    public interface IMessageRepository
    {
        Task Send(Message message);
    }

    public class MessageRepository : IMessageRepository
    {
        private readonly SmsVendorDbContext _context;

        public MessageRepository(SmsVendorDbContext context)
        {
            _context = context;
        }

        public async Task Send(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}
