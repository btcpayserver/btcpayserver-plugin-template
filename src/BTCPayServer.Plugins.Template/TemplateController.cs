using BTCPayServer.Abstractions.Constants;
using BTCPayServer.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTCPayServer.Plugins.Template;

[Area(Plugin.Area)]
[Authorize(AuthenticationSchemes = AuthenticationSchemes.Cookie, Policy = Policies.CanViewProfile)]
public class TemplateController : Controller
{
    [HttpGet("~/template")]
    public IActionResult Index()
    {
        return View();
    }
}
