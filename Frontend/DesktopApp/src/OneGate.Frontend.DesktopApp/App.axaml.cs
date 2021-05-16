using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OneGate.Frontend.Client;
using OneGate.Frontend.DesktopApp.ViewModels;
using OneGate.Frontend.DesktopApp.Views;

namespace OneGate.Frontend.DesktopApp
{
    public class App : Application
    {
        IOptions<OneGateClientOptions> ConnectionOptions { get; set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            InitializeConfiguration();
        }

        public void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            var options = new OneGateClientOptions(new Uri(configuration["EndpointUri"]),
                configuration["ClientKey"]);
            ConnectionOptions = Options.Create(options);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindowView
                {
                    DataContext = new MainWindowViewModel(ConnectionOptions),
                };
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
