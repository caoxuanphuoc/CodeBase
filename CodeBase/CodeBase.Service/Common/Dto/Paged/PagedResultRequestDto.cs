using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Service.Common.Dto
{
    public class PagedResultRequestDto
    {
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; } = 10;


    }
}
