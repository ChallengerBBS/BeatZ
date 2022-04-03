using Microsoft.AspNetCore.Mvc;

namespace BeatZ.Api.Controllers
{
    public class AlbumsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
