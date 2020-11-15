using erthsobes_api.Model;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace erthsobes_api
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
            _context.Attachments.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Attachment>> GetAttachments()
        {
            // Using the shadow property EF.Property<DateTime>(dataEventRecord)
            return await _context.Attachments.OrderByDescending(attachment => EF.Property<DateTime>(attachment, "UpdatedTimestamp")).ToListAsync();
        }
        
    }
}
