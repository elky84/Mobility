using Mobility.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobility.Protocols.Common
{
    public class Call
    {
        public int Id { get; set; }

        public Member Member { get; set; }

        public Position Position { get; set; }

        public Taxi Taxi { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime AssignedAt { get; set; }

        public DateTime CompletedAt { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CallState State { get; set; }
    }
}
