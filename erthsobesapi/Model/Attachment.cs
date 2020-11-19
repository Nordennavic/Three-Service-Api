using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace erthsobesapi.Model
{
    public class Attachment
    {
        [Key]
        public long id { get; set; }
        public string hash { get; set; }
    }
}
