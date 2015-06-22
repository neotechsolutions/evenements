using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using System;
#if DNX451
using IotWebApp.Events;
using Microsoft.ServiceBus.Messaging;
#endif

namespace IotWebApp
{
    public class Startup
    {

#if DNX451
        private static EventProcessorHost eventProcessorHost;
#endif

        public static IServiceProvider ServiceProvider;

        public Startup(IHostingEnvironment env)
        {
            // Setup configuration sources.
            var configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);
            configuration.AddEnvironmentVariables();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add Application settings to the services container.
            services.Configure<AppSettings>(Configuration.GetSubKey("AppSettings"));
#if DNX451
            string eventProcessorHostName = Guid.NewGuid().ToString();
            eventProcessorHost = new EventProcessorHost(eventProcessorHostName, "signalr", EventHubConsumerGroup.DefaultGroupName, Configuration["AppSettings:ServiceBusConnectionString"], Configuration["AppSettings:AzureStorageConnectionString"]);
            eventProcessorHost.RegisterEventProcessorAsync<Processor>();
#endif    
            // Add MVC & SignalR services to the services container.
            services.AddMvc();
            services.AddSignalR();

            ServiceProvider = services.BuildServiceProvider();
            return ServiceProvider;
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            // Add the logger.
            loggerfactory.AddConsole(minLevel: LogLevel.Warning);
            app.UseErrorHandler("/Home/Error");

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
            // Add SignalR
            app.UseSignalR();
        }
    }
}
