using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudDemo.Models
{
    public class LoginResponseModel
    {
        public string externalId { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string? lastName { get; set; }
        public string role { get; set; }
    }
}
