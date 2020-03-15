using Mobility.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mobility.Models
{
    public class MemberData
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public MemberType Type { get; set; }

        public string Token { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Protocols.Common.Member ToProtocol()
        {
            return new Protocols.Common.Member
            {
                Id = Id,
                Email = Email,
                Password = Password,
                Name = Name,
                Type = Type,
                CreatedAt = CreatedAt
            };
        }
    }
}
