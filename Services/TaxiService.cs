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
    public class TaxiService
    {
        private readonly DatabaseContext _databaseContext;

        public TaxiService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Protocols.Response.Taxi.Register> Register(Protocols.Request.Taxi.Register register)
        {
            var member = await _databaseContext.Members.FirstOrDefaultAsync(x => x.Token == register.Token);
            if (member == null)
            {
                throw new LogicException(Protocols.Code.ResultCode.NotFoundMember);
            }

            if (member.Type != Types.MemberType.Driver)
            {
                throw new LogicException(Protocols.Code.ResultCode.TaxiRegisterOnlyDriver);
            }

            var taxi = new TaxiData { Number = register.Number, Type = register.Type, Position = PositionData.FromProtocol(register.Position), Member = member };
            _databaseContext.Taxies.Add(taxi);

            await _databaseContext.SaveChangesAsync();

            return new Protocols.Response.Taxi.Register { Taxi = taxi.ToProtocol() };
        }

        public async Task<Protocols.Response.Taxi.UpdatePosition> UpdatePosition(Protocols.Request.Taxi.UpdatePosition updatePosition)
        {
            var taxi = await _databaseContext.Taxies.Include(x => x.Member).FirstOrDefaultAsync(x => x.Member.Token == updatePosition.Token);
            if (taxi == null)
            {
                throw new LogicException(Protocols.Code.ResultCode.NotFoundTaxi);
            }

            taxi.Position = PositionData.FromProtocol(updatePosition.Position);

            await _databaseContext.SaveChangesAsync();

            return new Protocols.Response.Taxi.UpdatePosition { Taxi = taxi.ToProtocol() };
        }
    }
}
