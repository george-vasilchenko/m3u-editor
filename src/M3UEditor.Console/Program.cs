using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using M3UEditor.App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace M3UEditor.Console
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var configurationRoot = GetConfiguration(args);
            using var host = CreateHostBuilder(args, configurationRoot).Build();

            var token = new CancellationToken();
            await ProcessAsync(host.Services, token);
            await host.RunAsync(token);
        }

        private static IConfigurationRoot GetConfiguration(string[] args) =>
            new ConfigurationBuilder()
                .AddIniFile("nl-live.settings.ini", false, true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

        private static async ValueTask ProcessAsync(IServiceProvider serviceProvider, CancellationToken token)
        {
            using var scope = serviceProvider.CreateScope();
            var playlistImporter = scope.ServiceProvider.GetRequiredService<IPlaylistImporter>();
            var playlistEditor = scope.ServiceProvider.GetRequiredService<IPlaylistEditor>();
            var appConfiguration = scope.ServiceProvider.GetRequiredService<IAppConfiguration>();
            var playlistExporter = scope.ServiceProvider.GetRequiredService<IPlaylistExporter>();

            var playlist = await playlistImporter.ImportAsync(appConfiguration.PlaylistImportPath, token);
            playlist = playlistEditor.RemoveAllGroupsExcept(playlist, appConfiguration.GroupsToKeep);
            await playlistExporter.ExportAsync(appConfiguration.PlaylistExportPath, playlist, token);
        }

        private static IHostBuilder CreateHostBuilder(string[] args, IConfigurationRoot configurationRoot)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services
                        .AddSingleton(GetAppConfiguration(configurationRoot))
                        .AddTransient<IPlaylistImporter, PlaylistImporter>()
                        .AddTransient<IPlaylistExporter, PlaylistExporter>()
                        .AddTransient<IPlaylistEditor, PlaylistEditor>());
        }

        private static IAppConfiguration GetAppConfiguration(IConfiguration configurationRoot) =>
            new AppConfiguration(
                configurationRoot["PlaylistImportPath"],
                configurationRoot["PlaylistExportPath"],
                configurationRoot.GetSection("GroupsToKeep").Get<List<string>>());
    }
}