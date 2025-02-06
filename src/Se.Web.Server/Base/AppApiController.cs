using Microsoft.AspNetCore.Mvc;

namespace Se.Web.Server.Base;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class AppApiController : ControllerBase
{
}
