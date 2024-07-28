using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Core
{
    public static class AssemblyReference
    {
        // Get class  Inheritance interface
        public static readonly Assembly Assembly = typeof(Assembly).Assembly;
    }
}
