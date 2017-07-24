using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeRun.Controllers
{
    public class CodeController : ViewBaseController
    {

        public IActionResult Index()
        {
            ViewBag.Title = "c";
            ViewBag.SetModel = "c_cpp";
            ViewBag.ReqUrl = "/api/code/run/c";
            return View("Index");
        }

        public IActionResult C()
        {
            ViewBag.Title = "c";
            ViewBag.SetModel = "c_cpp";
            ViewBag.ReqUrl = "/api/code/run/c";
            return View("Index");
        }

        public IActionResult Cpp()
        {
            ViewBag.SetModel = "c_cpp";
            ViewBag.Title = "cpp";
            ViewBag.ReqUrl = "/api/code/run/cpp";
            return View("Index");
        }

        public IActionResult Java()
        {

            ViewBag.Title = "java";
            ViewBag.SetModel = "java";
            ViewBag.ReqUrl = "/api/code/run/java";
            return View("Index");
        }

        public IActionResult Python()
        {
            ViewBag.Title = "python";
            ViewBag.SetModel = "python";
            ViewBag.ReqUrl = "/api/code/run/python";
            return View("Index");
        }

        public IActionResult Js()
        {
            ViewBag.Title = "javascript";
            ViewBag.SetModel = "javascript";
            ViewBag.ReqUrl = "scriptfunc:runjs()";
            return View("Index");
        }

        public IActionResult Csharp()
        {
            ViewBag.Title = "csharp";
            ViewBag.SetModel = "csharp";
            ViewBag.ReqUrl = "/api/code/run/csharp";
            return View("Index");
        }

        public IActionResult Nodejs()
        {
            ViewBag.Title = "nodejs";
            ViewBag.SetModel = "javascript";
            ViewBag.ReqUrl = "/api/code/run/nodejs";
            return View("Index");
        }
    }
}
