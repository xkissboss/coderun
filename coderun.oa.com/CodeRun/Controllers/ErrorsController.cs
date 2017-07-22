using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeRun.Controllers
{
    public class ErrorsController : Controller
    {

        [Route("errors/{statusCode}")]
        public IActionResult CustomError([FromRoute]int statusCode)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (statusCode == 404)
                return View("404");
            else if (statusCode == 500)
                return View("500");
            else if (statusCode == 401)
                return View("401");
            else if (statusCode == 502)
                return View("502");
            return View("common");
        }

        [Route("errors/handler")]
        public IActionResult Handler()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;
            return View("common");
        }
    }
}
