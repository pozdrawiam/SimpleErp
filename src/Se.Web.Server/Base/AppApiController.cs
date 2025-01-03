using Microsoft.AspNetCore.Mvc;

namespace Se.Web.Server.Base;

[ApiController]
[Route("[controller]/[action]")]
public abstract class AppApiController : ControllerBase
{
}
