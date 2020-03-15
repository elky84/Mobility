using Mobility.Protocols.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobility.Protocols.Request.Member
{
    public class SignIn : RequestHeader
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
