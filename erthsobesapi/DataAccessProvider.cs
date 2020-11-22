using erthsobesapi.Model;
using Microsoft.AspNetCore.Mvc;
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
            _context.Order_info.Add(order);
            await _context.SaveChangesAsync();
        }

        public dynamic GetStats()
        {
            var phoneCount = _context.Order_info.Where(p => p.type == "phone").Count();
            var phoneCountWithFile = _context.Order_info.Where(p => p.type == "phone").Where(p => p.attachment_id != 0).Count();
            var topPhones = ToFormat(_context.Order_info.Where(p => p.type == "phone").OrderByDescending(p => p.id).Take(10).ToArray());
            var emailCount = _context.Order_info.Where(p => p.type == "email").Count();
            var emailCountWithFile = _context.Order_info.Where(p => p.type == "email").Where(p => p.attachment_id != 0).Count();
            var topEmails = ToFormat(_context.Order_info.Where(p => p.type == "email").OrderByDescending(p => p.id).Take(10).ToArray());
            var otherCount = _context.Order_info.Where(p => p.type == "other").Count();
            var otherCountWithFile = _context.Order_info.Where(p => p.type == "other").Where(p => p.attachment_id != 0).Count();
            return new { phoneCount, phoneCountWithFile, topPhones, emailCount, emailCountWithFile, topEmails, otherCount, otherCountWithFile };
        }

        private string ToFormat(Order[] topOrders)
        {
            string top = "";
            if (topOrders.Length != 0) 
            {
                if (topOrders[0].type == "phone")
                {
                    foreach (var ord in topOrders)
                        top += ord.phoneNumber + ";";
                }
                else
                {
                    foreach (var ord in topOrders)
                        top += ord.email + ";";
                }
            }
            return top;

        }
        public async Task<Order> GetFileById(Guid id)
        {
            Order order = await _context.Order_info.SingleOrDefaultAsync(i => i.product_id == id);
            if (order != null)
                return order;
            else return null;
        }

        public async Task<Attachment> GetAttachment(long id)
        {
            // Using the shadow property EF.Property<DateTime>(dataEventRecord)
            Attachment attachment = await _context.Attachments.SingleOrDefaultAsync(i => i.id == id);
            if (attachment != null)
                return attachment;
            else return attachment;
        }

    }
}
