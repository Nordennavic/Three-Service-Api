using System;
using System.ComponentModel.DataAnnotations;

namespace erthsobesapi.Model
{
    [Serializable]
    public class Attachment
    {
        [Key]
        public long id { get; set; }
        public string hash { get; set; }
    }
}
