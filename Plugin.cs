using BTCPayServer.Abstractions.Contracts;
using BTCPayServer.Abstractions.Models;
using BTCPayServer.Abstractions.Services;
using BTCPayServer.Plugins.Template.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BTCPayServer.Plugins.Template;

public class Plugin : BaseBTCPayServerPlugin
{
    public override string Identifier { get; } = "BTCPayServer.Plugins.Template";
    public override string Name { get; } = "Plugin Template";
    public override string Description { get; } = "This is the plugin description";

    public override void Execute(IServiceCollection services)
    {
        services.AddSingleton<IUIExtension>(new UIExtension("TemplatePluginHeaderNav", "header-nav"));
        services.AddHostedService<ApplicationPartsLogger>();
        services.AddHostedService<PluginMigrationRunner>();
        services.AddSingleton<PluginService>();
        services.AddSingleton<PluginDbContextFactory>();
        services.AddDbContext<PluginDbContext>((provider, o) =>
        {
            PluginDbContextFactory factory = provider.GetRequiredService<PluginDbContextFactory>();
            factory.ConfigureBuilder(o);
        });
    }
}
