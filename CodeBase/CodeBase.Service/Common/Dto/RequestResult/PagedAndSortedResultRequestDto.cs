using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Service.Common.Dto
{
    public class PagedAndSortedResultRequestDto : PagedResultRequestDto, ISortedResultRequest
    {
        public string Sorting { get ; set; }
    }
}
