using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace erthsobesapi.Model
{
    public class Order
    {
        [Key]
        public long id { get; set; }
        public Guid product_id { get; set; }
        public string type { get; set; }
        public decimal cost { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string value { get; set; }
        [ForeignKey("Attachment")]
        public long attachment_id { get; set; }
    }
}
