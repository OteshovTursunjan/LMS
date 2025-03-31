using LMS.Domain.Common;
using LMS.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Entity
{
    public  class Contract : BaseEntity, IAuditedEntity
    {
        public int ContractNumber { get; set; }
        public ApplicationUser Account { get; set; }
        public Guid AccountID { get; set; }
        public TuitionFee TuitionFee { get; set; }
        public Guid TuitionFeeID { get; set; }
        public bool IsScholarship { get; set; }
        public string? CreatBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string? UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
