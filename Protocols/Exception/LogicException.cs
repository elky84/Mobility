using Mobility.Protocols.Code;
using Mobility.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Mobility.Protocols.Exception
{
    public class LogicException : System.Exception
    {
        public ResultCode ResultCode { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public string Detail { get; set; }

        public LogicException(ResultCode resultCode, HttpStatusCode httpStatusCode = System.Net.HttpStatusCode.InternalServerError, string detail = null)
        {
            ResultCode = resultCode;
            HttpStatusCode = httpStatusCode;
            Detail = detail;
        }
    }
}
