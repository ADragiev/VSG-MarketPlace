using Application.Helpers.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [Admin]
    public class BaseController : ControllerBase
    {
    }
}
