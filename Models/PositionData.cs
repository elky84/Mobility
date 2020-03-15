using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobility.Models
{
    public class PositionData
    {
        public double N { get; set; }

        public double E { get; set; }

        public Protocols.Common.Position ToProtocol()
        {
            return new Protocols.Common.Position
            {
                E = E,
                N = N
            };
        }

        public static PositionData FromProtocol(Protocols.Common.Position position)
        {
            return new PositionData
            {
                N = position.N,
                E = position.E
            };
        }
    }
}
