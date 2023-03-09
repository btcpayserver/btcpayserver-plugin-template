using System.Collections.Generic;
using System.Threading.Tasks;
using BTCPayServer.Abstractions.Constants;
using BTCPayServer.Client;
using BTCPayServer.Plugins.Template.Data;
using BTCPayServer.Plugins.Template.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTCPayServer.Plugins.Template;

[Route("~/plugins/template")]
[Authorize(AuthenticationSchemes = AuthenticationSchemes.Cookie, Policy = Policies.CanViewProfile)]
public class UIPluginController : Controller
{
    private readonly MyPluginService _PluginService;

    public UIPluginController(MyPluginService PluginService)
    {
        _PluginService = PluginService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        return View(new PluginPageViewModel { Data = await _PluginService.Get() });
    }
}

public class PluginPageViewModel
{
    public List<PluginData> Data { get; set; }
}
