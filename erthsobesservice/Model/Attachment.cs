using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace erthsobesservice.Model
{
    [Serializable]
    public class Attachment
    {
        public long id { get; set; }
        public string hash { get; set; }

        public override String ToString()
        {
            return String.Format("{{\"id\": {0}, \"hash\": \"{1}\"}}", id, hash);
        }
    }

    
}
