using Microsoft.AspNetCore.Mvc;

namespace DarAlforsan.Areas.Common.Controllers
{
    [Area("Common")]
    public class SessionController : Controller
    {
        public IActionResult Extend()
        {
            return Ok();
        }
    }
}