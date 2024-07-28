using CodeBase.Core.Common.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Core.Common
{
    public abstract class EntityAuditBase<TKey> : EntityBase<TKey>, IEntityAuditBase<TKey>
    {
        public bool IsDeleted { get ; set ; } = false;
        public DateTimeOffset? DeletedDate { get ; set ; }
        public DateTimeOffset CreatedDate { get ; set ; } = DateTimeOffset.Now;
        public DateTimeOffset? ModifiedDate { get ; set ; }
        public long? CreatedBy { get; set; } 
        public long? ModifiedBy { get ; set ; }
    }
}
