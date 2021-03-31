using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APM.Core.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public IActionResult ProductManager()
        {
            return View();
        }
    }
}