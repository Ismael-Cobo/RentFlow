using System.Reflection;

namespace RentFlow.Modules.Fleet.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
