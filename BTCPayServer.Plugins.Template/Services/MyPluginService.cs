using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BTCPayServer.Plugins.Template.Data;
using Microsoft.EntityFrameworkCore;

namespace BTCPayServer.Plugins.Template.Services;

public class MyPluginService
{
    private readonly MyPluginDbContextFactory _PluginDbContextFactory;

    public MyPluginService(MyPluginDbContextFactory PluginDbContextFactory)
    {
        _PluginDbContextFactory = PluginDbContextFactory;
    }

    public async Task AddTestDataRecord()
    {
        await using var context = _PluginDbContextFactory.CreateContext();

        await context.PluginRecords.AddAsync(new PluginData { Timestamp = DateTimeOffset.UtcNow });
        await context.SaveChangesAsync();
    }

    public async Task<List<PluginData>> Get()
    {
        await using var context = _PluginDbContextFactory.CreateContext();

        return await context.PluginRecords.ToListAsync();
    }
}

