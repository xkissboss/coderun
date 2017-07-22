using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeRun.Utils
{
    public class APIReturn : JsonResult
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public bool Success { get { return this.Code == 0; } }

        private APIReturn(int code, string message, object data) : base(new { code = code, message = message, data = data })
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

        public static APIReturn BuildSuccess(string message = "", object data = null)
        {
            return new APIReturn(0, message, data);
        }

        public static APIReturn BuildSuccess(object data = null)
        {
            return new APIReturn(0, "", data);
        }

        public static APIReturn BuildFail(string message = "")
        {
            return new APIReturn(99, message, null);
        }

        public static APIReturn BuildFail(int code = 99, string message = "")
        {
            return new APIReturn(code, message, null);
        }
    }
}
