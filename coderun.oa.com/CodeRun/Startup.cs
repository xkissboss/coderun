using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AppCore.Utils;
using AppCore.Build;
using AppCore.Build.Impl;
using AppCore.Run.Impl;
using AppCore.Run;
using AppService.Impl;
using AppService;
using Microsoft.Extensions.FileProviders;
using CodeRun.Middleware;
using NLog.Extensions.Logging;
using System.IO;

namespace CodeRun
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.

            services.AddScoped<IBuildCommand, BuildCommand>();
            services.AddScoped<IRunCommand, RunCommand>();
            services.AddScoped<ICoreService, CoreService>();
            var urls = Configuration.GetValue<string>("Cores").Split(',');
            if (urls != null && urls.Length > 0)
            {
                services.AddCors(options =>
                    options.AddPolicy("allowdomain",
                    builder => builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials())
                );
            }
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddNLog().AddDebug();

            string nlog = Path.Combine(Directory.GetCurrentDirectory(), "nlog.config");
            loggerFactory.ConfigureNLog(nlog);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/errors/handler");
            }

            app.UseCustomErrorMiddleware();
            app.UseCors("allowdomain");
            app.UseStaticFiles();
            StaticVariable.CODE_SAVE_PATH = Configuration.GetValue<string>("CodeSavePath");
            StaticVariable.RUN_MILL = Configuration.GetValue<int>("RunMill");
            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(Configuration.GetValue<string>("CodeAce")),
                RequestPath = "/ace",
                EnableDirectoryBrowsing = false
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
