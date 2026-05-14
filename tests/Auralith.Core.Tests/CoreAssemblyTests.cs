using System.Reflection;
using Shouldly;
using Xunit;

namespace Auralith.Core.Tests;

public sealed class CoreAssemblyTests
{
    [Fact]
    public void Core_assembly_loads()
    {
        var assembly = Assembly.Load("Auralith.Core");

        assembly.GetName().Name.ShouldBe("Auralith.Core");
    }
}
