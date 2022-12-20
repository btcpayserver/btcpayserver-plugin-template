using System.Threading;
using System.Threading.Tasks;
using BTCPayServer.Abstractions.Contracts;
using BTCPayServer.Plugins.Template.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BTCPayServer.Plugins.Template;

public class PluginMigrationRunner : IHostedService
{
    private readonly PluginDbContextFactory _PluginDbContextFactory;
    private readonly PluginService _PluginService;
    private readonly ISettingsRepository _settingsRepository;

    public PluginMigrationRunner(PluginDbContextFactory PluginDbContextFactory, ISettingsRepository settingsRepository,
        PluginService PluginService)
    {
        _PluginDbContextFactory = PluginDbContextFactory;
        _settingsRepository = settingsRepository;
        _PluginService = PluginService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        PluginDataMigrationHistory settings = await _settingsRepository.GetSettingAsync<PluginDataMigrationHistory>() ??
                                              new PluginDataMigrationHistory();
        await using PluginDbContext ctx = _PluginDbContextFactory.CreateContext();
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

