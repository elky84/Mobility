using Mobility.Protocols.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobility.Protocols.Request.Taxi
{
    public class UpdatePosition
    {
        public string Token { get; set; }

        public Position Position { get; set; }
    }
}
