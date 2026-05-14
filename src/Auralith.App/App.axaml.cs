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
            MediaOpenRequest.TryCreateFromCommandLine(StartupArgs, out var request, out _);
            desktop.MainWindow = new MainWindow(request);
        }

        base.OnFrameworkInitializationCompleted();
    }
}
