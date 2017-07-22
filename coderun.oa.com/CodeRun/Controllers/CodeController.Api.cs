using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeRun.Utils;
using AppService;
using AppCore.Utils;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeRun.Api.Controllers
{
    [Route("api/[controller]")]
    public class CodeController : ApiBaseController
    {
        private readonly ICoreService coreService;
        public CodeController(ICoreService coreService)
        {
            this.coreService = coreService;
        }

        [HttpPost("run/{act}")]
        public IActionResult C([FromRoute]string act, string code)
        {
            if (string.IsNullOrEmpty(act))
                return APIReturn.BuildFail("无效的请求");
            if (string.IsNullOrEmpty(code))
                return APIReturn.BuildFail("code不能为空");
            string codeStr = code.Base64ToString();
            if (string.IsNullOrEmpty(codeStr))
                return APIReturn.BuildFail("无效的code");

            CmResult cm = null;
            if ("c".Equals(act))
                cm = coreService.C(codeStr);
            else if ("cpp".Equals(act))
                cm = coreService.CPlusPlus(codeStr);
            else if ("java".Equals(act))
                cm = coreService.Java(codeStr);
            else if ("python".Equals(act))
                cm = coreService.Python(codeStr);
            else if ("csharp".Equals(act))
                cm = coreService.CSharp(codeStr);
            else if ("nodejs".Equals(act))
                cm = coreService.Nodejs(codeStr);
            else
                cm = CmResult.BuildFail("", "无效的请求");
            if (cm.Success)
                return APIReturn.BuildSuccess(cm.Message);
            return APIReturn.BuildFail(cm.ExecMsg);
        }

    }
}
