using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace R3ctApp.Api.Responses
{
    public class HireProcessResp
    {
        public long Id { get; set; }

        public string Job { get; set; }

        public string Name { get; set; }
        
        public string Phone { get; set; }
        
        public string Email { get; set; }
        
        public string Status { get; set; }
        
    }
}
