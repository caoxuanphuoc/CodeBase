using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Core.Common.Audit
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; } 
        DateTimeOffset?  DeletedDate{ get; set; }
    }
}
