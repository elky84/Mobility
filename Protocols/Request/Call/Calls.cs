using Mobility.Protocols.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobility.Protocols.Request.Call
{
    public class Calls : RequestHeader
    {
        public bool Receivable { get; set; }

        public int Page { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}
