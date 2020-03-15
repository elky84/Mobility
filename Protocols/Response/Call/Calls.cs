using Mobility.Protocols.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobility.Protocols.Response.Call
{
    public class Calls : ResponseHeader
    {
        public List<Common.Call> CallList { get; set; }
    }
}
