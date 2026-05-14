using Auralith.Playback;
using Shouldly;
using Xunit;

namespace Auralith.Playback.Tests;

public sealed class PlaybackConstraintsTests
{
    [Theory]
    [InlineData(-10, 0)]
    [InlineData(0, 0)]
    [InlineData(42.5, 42.5)]
    [InlineData(100, 100)]
    [InlineData(125, 100)]
    public void ClampVolume_keeps_value_inside_supported_range(double value, double expected)
    {
        PlaybackConstraints.ClampVolume(value).ShouldBe(expected);
    }

    [Fact]
    public void ClampPosition_returns_zero_for_negative_position()
    {
        PlaybackConstraints.ClampPosition(TimeSpan.FromSeconds(-5))
            .ShouldBe(TimeSpan.Zero);
    }

    [Fact]
    public void ClampPosition_keeps_non_negative_position()
    {
        var position = TimeSpan.FromSeconds(12.25);

        PlaybackConstraints.ClampPosition(position).ShouldBe(position);
    }
}
