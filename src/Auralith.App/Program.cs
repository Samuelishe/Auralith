using Avalonia;

namespace Auralith.App;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        App.StartupArgs = args;
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    private static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
    }
}
