using Mobility.Protocols.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobility.Protocols.Request.Taxi
{
    public class Register
    {
        public string Token { get; set; }

        public string Number { get; set; }

        public string Type { get; set; }

        public Position Position { get; set; }
    }
}
