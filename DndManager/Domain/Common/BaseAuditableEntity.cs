using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity //todo: fix db with fields
    {
        [NotMapped]
        public DateTimeOffset Created { get; set; }
        [NotMapped]
        public string? CreatedBy { get; set; }
        [NotMapped]
        public DateTimeOffset LastModified { get; set; }
        [NotMapped]
        public string? LastModifiedBy { get; set; }
    }
}
