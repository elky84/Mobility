using Mobility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Mobility.Protocols.Exception;

namespace Mobility.Services
{
    public class MemberService
    {
        private readonly DatabaseContext _databaseContext;

        public MemberService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Protocols.Response.Member.SignUp> SignUp(Protocols.Request.Member.SignUp signUp)
        {
            var existMember = await _databaseContext.Members.FirstOrDefaultAsync(x => x.Email == signUp.Email);
            if (existMember != null)
            {
                throw new LogicException(Protocols.Code.ResultCode.AlreadySignedEmail);
            }

            var member = new MemberData { Email = signUp.Email, Name = signUp.Name, Password = signUp.Password, Type = signUp.Type, Token = Guid.NewGuid().ToString("N") };

            _databaseContext.Members.Add(member);

            await _databaseContext.SaveChangesAsync();

            return new Protocols.Response.Member.SignUp { Token = member.Token };
        }

        public async Task<Protocols.Response.Member.SignIn> SignIn(Protocols.Request.Member.SignIn signIn)
        {
            var member = await _databaseContext.Members.FirstOrDefaultAsync(x => x.Email == signIn.Email);
            if (member == null)
            {
                throw new LogicException(Protocols.Code.ResultCode.NotFoundMemberByEmail);
            }


            if (member.Password != signIn.Password)
            {
                throw new LogicException(Protocols.Code.ResultCode.NotMatchedPassword);
            }

            member.Token = Guid.NewGuid().ToString("N");
            await _databaseContext.SaveChangesAsync();

            return new Protocols.Response.Member.SignIn { Token = member.Token };
        }
    }
}
