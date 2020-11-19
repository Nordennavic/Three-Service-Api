using System.ComponentModel.DataAnnotations;

namespace erthsobesapi.Model
{
    public class Attachment
    {
        [Key]
        public long id { get; set; }
        public string hash { get; set; }
    }
}
