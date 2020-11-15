using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace erthsobes_api.Model
{
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        [Key]
        public long id { get; set;}
        public Guid product_id { get; set; }
        public string type { get; set; }
        public decimal cost { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string value { get; set; }
        public Attachment attachment_id { get; set; }
    }
}
