using BTCPayServer.Abstractions.Contracts;
using BTCPayServer.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace BTCPayServer.Plugins.Template;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyPluginDbContext>
{
    public MyPluginDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<MyPluginDbContext> builder = new DbContextOptionsBuilder<MyPluginDbContext>();

        builder.UseSqlite("Data Source=temp.db");

        return new MyPluginDbContext(builder.Options, true);
    }
}

public class MyPluginDbContextFactory : BaseDbContextFactory<MyPluginDbContext>
{
    public MyPluginDbContextFactory(IOptions<DatabaseOptions> options) : base(options, "BTCPayServer.Plugins.Template")
    {
    }

    public override MyPluginDbContext CreateContext()
    {
        DbContextOptionsBuilder<MyPluginDbContext> builder = new DbContextOptionsBuilder<MyPluginDbContext>();
        ConfigureBuilder(builder);
        return new MyPluginDbContext(builder.Options);
    }
}
