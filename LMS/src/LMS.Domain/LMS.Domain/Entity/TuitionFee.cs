using LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Entity
{
    public  class TuitionFee : BaseEntity, IAuditedEntity
    {
        public Faculty Faculty { get; set; }
        public Guid FacultyId { get; set; }
        public int Summa { get; set; }
        public string? CreatBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string? UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
