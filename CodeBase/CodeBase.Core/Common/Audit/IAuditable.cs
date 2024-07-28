using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Core.Common.Audit
{
    public interface IAuditable : ISoftDelete, IDateTracking, IUserTracking
    {
    }
}
