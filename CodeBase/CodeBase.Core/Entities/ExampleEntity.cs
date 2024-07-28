using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBase.Core.Common;

namespace CodeBase.Core.Entities
{
    public class ExampleEntity : EntityAuditBase<int>
    {
        //Inhern EntityAuditBase<int> it's mean we have Id property with type int
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
