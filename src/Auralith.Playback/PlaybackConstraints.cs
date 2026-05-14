namespace Auralith.Playback;

public static class PlaybackConstraints
{
    public const double MinVolume = 0;
    public const double MaxVolume = 100;

    public static double ClampVolume(double value)
    {
        return double.IsFinite(value) ? Math.Clamp(value, MinVolume, MaxVolume) : MinVolume;
    }

    public static TimeSpan ClampPosition(TimeSpan value)
    {
        return value < TimeSpan.Zero ? TimeSpan.Zero : value;
    }
}
