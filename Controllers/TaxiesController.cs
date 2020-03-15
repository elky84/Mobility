using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mobility.Services;

namespace Mobility.Controllers
{

    [ApiController]
    [Route("Taxies")]
    public class TaxiesController : ControllerBase
    {
        private readonly TaxiService _taxiService;

        public TaxiesController(TaxiService taxiService)
        {
            _taxiService = taxiService;
        }

        [HttpPost("Register")]
        public async Task<Protocols.Response.Taxi.Register> Register([FromBody]Protocols.Request.Taxi.Register register)
        {
            return await _taxiService.Register(register);
        }


        [HttpPut("Position")]
        public async Task<Protocols.Response.Taxi.UpdatePosition> UpdatePosition([FromBody]Protocols.Request.Taxi.UpdatePosition updatePosition)
        {
            return await _taxiService.UpdatePosition(updatePosition);
        }
    }

}
