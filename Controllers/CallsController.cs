using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mobility.Services;

namespace Mobility.Controllers
{

    [ApiController]
    [Route("Calls")]
    public class CallsController : ControllerBase
    {
        private readonly CallService _CallService;

        public CallsController(CallService CallService)
        {
            _CallService = CallService;
        }

        [HttpPost]
        public async Task<Protocols.Response.Call.Call> Call([FromBody]Protocols.Request.Call.Call call)
        {
            return await _CallService.Call(call);
        }


        [HttpGet]
        public async Task<Protocols.Response.Call.Calls> Calls([FromQuery]Protocols.Request.Call.Calls calls)
        {
            return await _CallService.Calls(calls);
        }

        [HttpPost("Receive/{id}")]
        public async Task<Protocols.Response.Call.Receive> Receive([FromBody]Protocols.Request.Call.Receive receive, int id)
        {
            return await _CallService.Receive(id, receive);
        }

        [HttpPost("Complete/{id}")]
        public async Task<Protocols.Response.Call.Complete> Complete([FromBody]Protocols.Request.Call.Complete complete, int id)
        {
            return await _CallService.Complete(id, complete);
        }
    }

}
