using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace erthsobes_api.Model
{
    public class Attachment
    {
        [Key]
        public long id;
        public string hash;
    }
}
