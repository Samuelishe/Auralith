namespace Auralith.Core;

public sealed class MediaOpenRequest
{
    private MediaOpenRequest(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public static bool TryCreate(string? path, out MediaOpenRequest? request, out string? error)
    {
        request = null;
        error = null;

        if (string.IsNullOrWhiteSpace(path))
        {
            error = "No media file path was provided.";
            return false;
        }

        string fullPath;
        try
        {
            fullPath = System.IO.Path.GetFullPath(path);
        }
        catch (Exception ex) when (ex is ArgumentException or NotSupportedException or PathTooLongException)
        {
            error = "Media file path is invalid.";
            return false;
        }

        if (Directory.Exists(fullPath))
        {
            error = "Folder opening is not supported in this spike.";
            return false;
        }

        if (!File.Exists(fullPath))
        {
            error = "Media file does not exist.";
            return false;
        }

        request = new MediaOpenRequest(fullPath);
        return true;
    }

    public static bool TryCreateFromCommandLine(string[] args, out MediaOpenRequest? request, out string? error)
    {
        var firstPath = args.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x) && !x.StartsWith('-'));
        return TryCreate(firstPath, out request, out error);
    }
}
