using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    class ProficiencyCreatedEvent : BaseEvent
    {
        public Proficiency Proficiency { get; init; }
        public ProficiencyCreatedEvent(Proficiency proficiency)
        {
            Proficiency = proficiency;
        }
    }
}
