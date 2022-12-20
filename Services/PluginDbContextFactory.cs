using BTCPayServer.Abstractions.Contracts;
using BTCPayServer.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace BTCPayServer.Plugins.Template;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PluginDbContext>
{
    public PluginDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<PluginDbContext> builder = new DbContextOptionsBuilder<PluginDbContext>();

        builder.UseSqlite("Data Source=temp.db");

        return new PluginDbContext(builder.Options, true);
    }
}

public class PluginDbContextFactory : BaseDbContextFactory<PluginDbContext>
{
    public PluginDbContextFactory(IOptions<DatabaseOptions> options) : base(options, "BTCPayServer.Plugins.Template")
    {
    }

    public override PluginDbContext CreateContext()
    {
        DbContextOptionsBuilder<PluginDbContext> builder = new DbContextOptionsBuilder<PluginDbContext>();
        ConfigureBuilder(builder);
        return new PluginDbContext(builder.Options);
    }
}
