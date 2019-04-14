using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace OpenShiftApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AppSettings>(Program.Configuration.GetSection("App"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration, IOptions<AppSettings> settings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var json = JsonConvert.SerializeObject(new {settings.Value, env});
            app.Run(async (context) => { await context.Response.WriteAsync(json); });
        }
    }

    public class AppSettings
    {
        public string SimpleKey { get; set; }
        public string[] ArrayKey { get; set; }
        public AppSettingKey ObjectKey { get; set; }
    }

    public class AppSettingKey
    {
        public string SimpleKey2 { get; set; }
        public string[] ArrayKey2 { get; set; }
    }
}