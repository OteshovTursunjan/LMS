using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Model;

public  class PageResult<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public int TotalPage => (int)Math.Ceiling((double)TotalCount / PageSize);
}
