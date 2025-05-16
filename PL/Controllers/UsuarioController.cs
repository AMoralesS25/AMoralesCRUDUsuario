using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
    }
}
