using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobility.Protocols
{
    public class Action
    {
        public int MobilityId { get; set; }

        public int SkillId { get; set; }

        public int? TargetIndex { get; set; }
    }
}
