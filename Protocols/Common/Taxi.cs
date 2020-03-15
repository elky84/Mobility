using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobility.Protocols.Common
{
    public class Taxi
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Type { get; set; }

        public Member Member { get; set; }

        public Position Position { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
