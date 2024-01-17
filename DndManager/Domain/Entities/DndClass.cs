using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DndClass : BaseAuditableEntity
    {
        public string Name { get; set; }
        public int Lvl { get; set; }
        public string SubclassName { get; set; }

        public string PcId { get; set; }
        public Pc Pc { get; set; } = null!;
    }
}
