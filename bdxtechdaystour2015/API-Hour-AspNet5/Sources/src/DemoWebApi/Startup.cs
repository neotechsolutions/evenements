using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc.Xml;
using Microsoft.Framework.DependencyInjection;
using DemoWebApi.Models;
using Microsoft.AspNet.Mvc;

namespace DemoWebApi
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.ConfigureMvc(options =>
            {
                options.InputFormatters.Add(new XmlSerializerInputFormatter());
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });
            services.AddSingleton<IRepository, Repository>();
            services.AddSignalR();
            services.AddCors();
            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(x => x.AllowAnyOrigin());
            app.UseSignalR();
            app.UseSwagger();
            app.UseMvc();
        }
    }
}
