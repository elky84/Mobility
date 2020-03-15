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
    public class CallService
    {
        private readonly DatabaseContext _databaseContext;

        public CallService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Protocols.Response.Call.Call> Call(Protocols.Request.Call.Call call)
        {
            var member = await _databaseContext.Members.FirstOrDefaultAsync(x => x.Token == call.Token);
            if (member == null)
            {
                throw new LogicException(Protocols.Code.ResultCode.NotFoundMember);
            }

            if (member.Type != Types.MemberType.User)
            {
                throw new LogicException(Protocols.Code.ResultCode.CallOnlyUser);
            }

            var existCalls = await _databaseContext.Calls.Where(x => x.MemberDataId == member.Id && x.State != Types.CallState.Completed).ToListAsync();
            if (existCalls.Any())
            {
                if (null != existCalls.FirstOrDefault(x => x.State == Types.CallState.Receive))
                {
                    throw new LogicException(Protocols.Code.ResultCode.AlreadyReceivedCall);
                }
                else if (null != existCalls.FirstOrDefault(x => x.State == Types.CallState.Wait))
                {
                    throw new LogicException(Protocols.Code.ResultCode.AlreadyRegisteredCall);
                }
            }


            var callData = new CallData { Member = member, State = Types.CallState.Wait, CreatedAt = DateTime.Now, Position = PositionData.FromProtocol(call.Position) };
            _databaseContext.Calls.Add(callData);

            await _databaseContext.SaveChangesAsync();

            return new Protocols.Response.Call.Call { Content = callData.ToProtocol() };
        }

        public async Task<Protocols.Response.Call.Calls> Calls(Protocols.Request.Call.Calls calls)
        {
            var callDatas = await _databaseContext.Calls
                .Include(x => x.Member)
                .Include(x => x.Taxi)
                    .ThenInclude(x => x.Member)
                .Where(x => (calls.Receivable ? x.State == Types.CallState.Wait : true))
                .OrderByDescending(x => x.CreatedAt)
                .Skip(calls.Offset * calls.Page)
                .Take(calls.Limit)
                .ToListAsync();
            if (callDatas == null || callDatas.Count <= 0)
            {
                // 오류를 내야 할지, 빈 데이터를 줘야할지는 FE와 연동에 따라 다름.
                return new Protocols.Response.Call.Calls { };
            }

            return new Protocols.Response.Call.Calls { CallList = callDatas.ConvertAll(x => x.ToProtocol()) };
        }

        public async Task<Protocols.Response.Call.Receive> Receive(int id, Protocols.Request.Call.Receive receive)
        {
            var taxi = await _databaseContext.Taxies.Include(x => x.Member).FirstOrDefaultAsync(x => x.Member.Token == receive.Token);
            if (taxi == null)
            {
                throw new LogicException(Protocols.Code.ResultCode.NotFoundTaxi);
            }

            var existCall = await _databaseContext.Calls
                .Include(x => x.Taxi)
                .FirstOrDefaultAsync(x => x.Taxi != null && x.Taxi.Id == taxi.Id);
            if (existCall != null)
            {
                throw new LogicException(Protocols.Code.ResultCode.AlreadyReceivedCallByTaxi);
            }

            var call = await _databaseContext.Calls.FirstOrDefaultAsync(x => x.Id == id);
            if (call == null)
            {
                throw new LogicException(Protocols.Code.ResultCode.NotFoundCall);
            }

            if (call.State != Types.CallState.Wait)
            {
                throw new LogicException(Protocols.Code.ResultCode.CallReceiveOnlyWait);
            }

            call.AssignedAt = DateTime.Now;
            call.Taxi = taxi;
            call.State = Types.CallState.Receive;

            await _databaseContext.SaveChangesAsync();

            return new Protocols.Response.Call.Receive { Content = call.ToProtocol() };
        }

        public async Task<Protocols.Response.Call.Complete> Complete(int id, Protocols.Request.Call.Complete complete)
        {
            var taxi = await _databaseContext.Taxies.Include(x => x.Member).FirstOrDefaultAsync(x => x.Member.Token == complete.Token);
            if (taxi == null)
            {
                throw new LogicException(Protocols.Code.ResultCode.NotFoundTaxi);
            }

            var call = await _databaseContext.Calls.FirstOrDefaultAsync(x => x.Id == id);
            if (call == null)
            {
                throw new LogicException(Protocols.Code.ResultCode.NotFoundCall);
            }

            if (call.State != Types.CallState.Receive)
            {
                throw new LogicException(Protocols.Code.ResultCode.CallCompleteOnlyReceive);
            }

            call.CompletedAt = DateTime.Now;
            call.Taxi = null;
            call.State = Types.CallState.Completed;

            await _databaseContext.SaveChangesAsync();

            return new Protocols.Response.Call.Complete { Content = call.ToProtocol() };
        }
    }
}
