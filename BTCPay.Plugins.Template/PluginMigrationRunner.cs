using System.Threading;
using System.Threading.Tasks;
using BTCPayServer.Abstractions.Contracts;
using BTCPayServer.Plugins.Template.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BTCPayServer.Plugins.Template;

public class PluginMigrationRunner : IHostedService
{
    private readonly MyPluginDbContextFactory _PluginDbContextFactory;
    private readonly MyPluginService _PluginService;
    private readonly ISettingsRepository _settingsRepository;

    public PluginMigrationRunner(
        ISettingsRepository settingsRepository,
        MyPluginDbContextFactory PluginDbContextFactory,
        MyPluginService PluginService)
    {
        _settingsRepository = settingsRepository;
        _PluginDbContextFactory = PluginDbContextFactory;
        _PluginService = PluginService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        PluginDataMigrationHistory settings = await _settingsRepository.GetSettingAsync<PluginDataMigrationHistory>() ??
                                              new PluginDataMigrationHistory();
        await using var ctx = _PluginDbContextFactory.CreateContext();
        await ctx.Database.MigrateAsync(cancellationToken);

        // settings migrations
        if (!settings.UpdatedSomething)
        {
            settings.UpdatedSomething = true;
            await _settingsRepository.UpdateSetting(settings);
        }

        // test record
        await _PluginService.AddTestDataRecord();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public class PluginDataMigrationHistory
    {
        public bool UpdatedSomething { get; set; }
    }
}

