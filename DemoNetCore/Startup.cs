using AddRegistService;
using BaseCore.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;


using System.Net.Http;
using NLog.Extensions.Logging;
using NLog.Web;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Internal;
using BaseCore.Untity;
using DemoNetCore.Controller.Filters;
using Microsoft.Extensions.FileProviders;

namespace DemoNetCore
{
    public class Startup
    {
       //public static ILoggerRepository repository { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //string conn = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseMySql(conn));
            services.InjectionService();
            services.AddOptions();
            services.Configure<DBConfig>(Configuration.GetSection("DBConfig"));
            //repository = LogManager.CreateRepository("NETCoreRepository");
            //XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));   
            //services.AddMvc(o => o.Filters.Add<MyFilter>());//异常过滤器全局


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            var p = Configuration.GetSection("Redis");
             var k= p.GetValue<string>("Value");
            app.UseStaticFiles();
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");         
            app.UseMvc(routes =>
            {
                routes.Routes.Add(new AgentRoute(app.ApplicationServices, "Contact.cshtml"));

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
          Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
                RequestPath = new PathString("/StaticFiles")
            });
        }
    }
    //扩展路由
    public class AgentRoute : IRouter
    {
        private readonly string[] _urls;
        private readonly IRouter _mvcRoute;

        public AgentRoute(IServiceProvider services, params string[] urls)
        {
            _urls = urls;
            _mvcRoute = services.GetRequiredService<MvcRouteHandler>();
        }
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }

        public async Task RouteAsync(RouteContext context)
        {
            var requestedUrl = context.HttpContext.Request.Path.Value.TrimEnd('/');
            if (_urls.Contains(requestedUrl, StringComparer.OrdinalIgnoreCase))
            {
                context.RouteData.Values["controller"] = "home";
                context.RouteData.Values["action"] = "Message";
                await _mvcRoute.RouteAsync(context);
            }
        }
    }
}
