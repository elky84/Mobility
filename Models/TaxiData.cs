using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mobility.Models
{
    public class TaxiData
    {
        [Key]
        public int Id { get; set; }

        public string Number { get; set; }

        public string Type { get; set; }

        [ForeignKey("Member")]
        public int MemberId { get; set; }

        public MemberData Member { get; set; }

        public PositionData Position { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Protocols.Common.Taxi ToProtocol()
        {
            return new Protocols.Common.Taxi
            {
                Id = Id,
                Number = Number,
                Type = Type,
                Member = Member?.ToProtocol(),
                Position = Position?.ToProtocol(),
                CreatedAt = CreatedAt
            };
        }
    }
}
