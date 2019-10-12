using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Site.Controllers
{
    public class PlanoEnsinoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}