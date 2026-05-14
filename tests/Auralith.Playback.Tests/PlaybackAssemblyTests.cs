using System.Reflection;
using Shouldly;
using Xunit;

namespace Auralith.Playback.Tests;

public sealed class PlaybackAssemblyTests
{
    [Fact]
    public void Playback_assembly_loads()
    {
        var assembly = Assembly.Load("Auralith.Playback");

        assembly.GetName().Name.ShouldBe("Auralith.Playback");
    }
}
