using Auralith.Core;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace Auralith.App;

public sealed partial class App : Application
{
    internal static string[] StartupArgs { get; set; } = [];

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Report($"Startup args count: {StartupArgs.Length}");
            foreach (var arg in StartupArgs)
            {
                Report($"Startup arg: {arg}");
            }

            if (MediaOpenRequest.TryCreateFromCommandLine(StartupArgs, out var request, out var error))
            {
                Report($"Startup media request accepted: {request?.Path}");
            }
            else
            {
                Report($"Startup media request not created: {error}");
            }

            desktop.MainWindow = new MainWindow(request);
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static void Report(string message)
    {
        var fullMessage = $"[Auralith.App] {message}";
        Console.WriteLine(fullMessage);
        System.Diagnostics.Debug.WriteLine(fullMessage);
    }
}
