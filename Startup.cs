using Kudvenkat_.NET_Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kudvenkat_.NET_Core
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDBContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection"))); //---> this is to
            //register local AppDbContext class and also configuring the DB and its connection string, going to be used, in this case SQL Server.
            services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlDataContractSerializerFormatters();
            services.AddScoped<IEmployeeRespository, SQLEmployeeRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {             
               app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Error");

               //app.UseStatusCodePages(); //---> this will throw a simple error message saying error code 404 : page not found 
               app.UseStatusCodePagesWithRedirects("/Error/{0}"); //---> with this method we can intercept and call whatever customized
               //html page we want to call | once it get 404 error from the route service it redirects a request to mvc route of error controller
               //and it shows 200 status code in the network.

                //app.UseStatusCodePagesWithReExecute("/Error/{0}"); //--->once it recieves 404 from routing it does not send a new request but reexcute
                // the same request and make it 404, which kind of helps to identify which request was failed. This method must be prefered over later

                                                                   
            }

            //if (env.IsDevelopment())
            //{
            //    DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
            //    developerExceptionPageOptions.SourceCodeLineCount = 10;

            //    app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            //}

            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("foo.html");

            //app.UseDefaultFiles(defaultFilesOptions);

            //app.UseStaticFiles();

            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");

            //app.UseFileServer(fileServerOptions);

            //app.UseFileServer();

            app.UseStaticFiles();

            //app.UseMvcWithDefaultRoute(); //---> mvc + default route configured
            //app.UseMvc( 

            //    routes => { routes.MapRoute("default", "{controller = Home}/{action = Index}/{id?}");  } //---> putting controller = Home and similarly action : localhost:3443// loads home page

            //    ); //--->NoContentResult default route configured,so we need to set it up by ourself. note : this is also called conventional routing

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("hello \n");
            //    await next();
            //});

            app.UseMvc(); //---> for using attribute routing we keep it clean and not routes parameter. 

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World"); //---> this will run at the end and will throw whatever there in it.
            //});
        }
    }
}
