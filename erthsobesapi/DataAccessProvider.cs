using erthsobesapi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace erthsobesapi
{
    public class DataAccessProvider
    {
        private readonly OrdersContext _context;
        private readonly ILogger _logger;

        public DataAccessProvider(OrdersContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("DataAccessPostgreSqlProvider");
        }

        public async Task AddAttachment(Attachment attachment)
        {
            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Attachment> GetFileById(Guid id)
        {
            Order order = await _context.Orders.SingleOrDefaultAsync(i => i.product_id == id);
            return order.attachment_id;
        }

        public async Task<List<Attachment>> GetAttachments()
        {
            // Using the shadow property EF.Property<DateTime>(dataEventRecord)
            return await _context.Attachments.OrderByDescending(attachment => EF.Property<DateTime>(attachment, "UpdatedTimestamp")).ToListAsync();
        }

    }
}
