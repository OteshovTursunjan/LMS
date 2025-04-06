using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Entity;

public class ExamLogs
{
    public Guid id { get; set; }
    public string Message { get; set; }
    public DateTime DateTime { get; set; }
}
