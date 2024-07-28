using System.Reflection;

namespace CodeBase.Service
{
    public static class AssemblyReference
    {
        // Get class  Inheritance interface
        public static readonly Assembly Assembly = typeof(Assembly).Assembly;
    }
}
