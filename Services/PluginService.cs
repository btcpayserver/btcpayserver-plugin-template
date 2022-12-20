using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BTCPayServer.Plugins.Template.Data;
using Microsoft.EntityFrameworkCore;

namespace BTCPayServer.Plugins.Template.Services;

public class PluginService
{
    private readonly PluginDbContextFactory _PluginDbContextFactory;

    public PluginService(PluginDbContextFactory PluginDbContextFactory)
    {
        _PluginDbContextFactory = PluginDbContextFactory;
    }

    public async Task AddTestDataRecord()
    {
        await using PluginDbContext context = _PluginDbContextFactory.CreateContext();

        await context.PluginRecords.AddAsync(new PluginData { Timestamp = DateTimeOffset.UtcNow });
        await context.SaveChangesAsync();
    }

    public async Task<List<PluginData>> Get()
    {
        await using PluginDbContext context = _PluginDbContextFactory.CreateContext();

        return await context.PluginRecords.ToListAsync();
    }
}

