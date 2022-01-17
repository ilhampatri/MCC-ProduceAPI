using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.View_Models
{
    public class JWTtokenVM
    {
        public HttpStatusCode status { get; set; }
        public string tokenId { get; set; }
        public string message { get; set; }
    }
}
