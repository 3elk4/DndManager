using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Feat : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string SourceType { get; set; }
        public string Definition { get; set; }

        public string PcId { get; set; }
        public Pc Pc { get; set; } = null!;
    }
}
