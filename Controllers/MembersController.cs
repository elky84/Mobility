using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mobility.Services;

namespace Mobility.Controllers
{

    [ApiController]
    [Route("Members")]
    public class MembersController : ControllerBase
    {
        private readonly MemberService _memberService;

        public MembersController(MemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost("SignUp")]
        public async Task<Protocols.Response.Member.SignUp> SignUp([FromBody]Protocols.Request.Member.SignUp signUp)
        {
            return await _memberService.SignUp(signUp);
        }


        [HttpPost("SignIn")]
        public async Task<Protocols.Response.Member.SignIn> SignIn([FromBody]Protocols.Request.Member.SignIn signIn)
        {
            return await _memberService.SignIn(signIn);
        }
    }

}
