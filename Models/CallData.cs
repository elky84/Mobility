using Mobility.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mobility.Models
{
    public class CallData
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Member")]
        public int MemberDataId { get; set; }

        public MemberData Member { get; set; }

        public PositionData Position { get; set; }

        [ForeignKey("Taxi")]
        public int? TaxiDataId { get; set; }

        public TaxiData Taxi { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime AssignedAt { get; set; }

        public DateTime CompletedAt { get; set; }

        public CallState State { get; set; }

        public Protocols.Common.Call ToProtocol()
        {
            return new Protocols.Common.Call
            {
                Id = Id,
                Member = Member?.ToProtocol(),
                Position = Position?.ToProtocol(),
                Taxi = Taxi?.ToProtocol(),
                CreatedAt = CreatedAt,
                AssignedAt = AssignedAt,
                CompletedAt = CompletedAt,
                State = State,
            };
        }
    }
}
