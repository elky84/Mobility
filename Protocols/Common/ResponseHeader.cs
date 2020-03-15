using Mobility.Protocols.Code;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mobility.Protocols.Common
{
    public class ResponseHeader
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ResultCode Code { get; set; } = ResultCode.Success;

        public int StatusCode { get; set; }
    }
}
