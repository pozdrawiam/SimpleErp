using Microsoft.AspNetCore.Mvc;

namespace Se.Web.Server.Shared;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class AppApiController : ControllerBase
{
}
