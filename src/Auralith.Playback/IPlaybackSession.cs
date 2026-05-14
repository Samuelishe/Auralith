namespace Auralith.Playback;

public interface IPlaybackSession
{
    string? CurrentMediaPath { get; }
    bool IsPaused { get; set; }
    TimeSpan Position { get; }
    TimeSpan Duration { get; }
    double Volume { get; set; }

    void Open(string path);
    void Stop();
    void Seek(TimeSpan position);
}
