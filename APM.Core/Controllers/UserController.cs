using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APM.Core.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult UserManager()
        {
            return View();
        }
    }
}
