using Mobility.Protocols.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobility.Protocols.Request.Call
{
    public class Receive : RequestHeader
    {
        public string Token { get; set; }
    }
}
