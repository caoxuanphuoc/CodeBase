using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Core.Common.Audit
{
    public interface IUserTracking
    {
        long? CreatedBy { get; set; }
        long? ModifiedBy { get; set; }
    }
}
