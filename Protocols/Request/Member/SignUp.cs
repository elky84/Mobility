using Mobility.Protocols.Common;
using Mobility.Types;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mobility.Protocols.Request.Member
{
    public class SignUp : RequestHeader
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MemberType Type { get; set; }
    }
}
