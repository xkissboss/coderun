using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CodeRun.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            
            string exceptionMsg = "";
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                exceptionMsg = ex.Message;
            }
            finally
            {
                var statusCode = httpContext.Response.StatusCode;
                bool isApi = httpContext.Request.Path.Value.StartsWith("/api/");

                if (!string.IsNullOrEmpty(exceptionMsg) || statusCode != 200)
                {
                    var msg = "";
                    if (isApi)
                    {
                        if (statusCode == 401)
                            msg = "无权限";
                        else if (statusCode == 404)
                            msg = "api不存在";
                        else if (statusCode == 500)
                            msg = "服务器内部错误";
                        else
                            msg = "未知异常，请稍后再试";
                        await HandleExceptionAsync(httpContext, statusCode, msg);
                    }
                    else
                    {
                        if (statusCode == 401 || statusCode == 404 || statusCode == 500)
                        {
                            httpContext.Request.Path = $"/errors/{statusCode}";
                            await _next.Invoke(httpContext);
                        }
                        if (!string.IsNullOrEmpty(exceptionMsg))
                        {
                            httpContext.Request.Path = $"/errors/0";
                            await _next.Invoke(httpContext);
                        }
                    }
                }

            }

        }
        private Task HandleExceptionAsync(HttpContext httpContext, int statusCode, string msg)
        {
            var data = new { code = statusCode, data = "", message = msg };
            var result = JsonConvert.SerializeObject(data);
            httpContext.Response.ContentType = "application/json;charset=utf-8";
            return httpContext.Response.WriteAsync(result);
        }

    }

    public static class CustomErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomErrorMiddleware>();
        }
    }
}
