using CodeBase.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Service.Handlers.V1.Example.Dto
{
    public class ExampleDto : EntityBase<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
