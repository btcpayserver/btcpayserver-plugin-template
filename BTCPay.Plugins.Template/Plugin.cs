using BTCPayServer.Abstractions.Contracts;
using BTCPayServer.Abstractions.Models;
using BTCPayServer.Abstractions.Services;
using BTCPayServer.Plugins.Template.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BTCPayServer.Plugins.Template;

public class Plugin : BaseBTCPayServerPlugin
{
    public override IBTCPayServerPlugin.PluginDependency[] Dependencies { get; } = new[]
    {
        new IBTCPayServerPlugin.PluginDependency { Identifier = nameof(BTCPayServer), Condition = ">=1.8.2" }
    };

    public override void Execute(IServiceCollection services)
    {
        services.AddSingleton<IUIExtension>(new UIExtension("TemplatePluginHeaderNav", "header-nav"));
        services.AddHostedService<ApplicationPartsLogger>();
        services.AddHostedService<PluginMigrationRunner>();
        services.AddSingleton<MyPluginService>();
        services.AddSingleton<MyPluginDbContextFactory>();
        services.AddDbContext<MyPluginDbContext>((provider, o) =>
        {
            MyPluginDbContextFactory factory = provider.GetRequiredService<MyPluginDbContextFactory>();
            factory.ConfigureBuilder(o);
        });
    }
}
