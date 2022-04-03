using Microsoft.AspNetCore.Mvc;

namespace BeatZ.Api.Controllers
{
    public class ArtistsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
