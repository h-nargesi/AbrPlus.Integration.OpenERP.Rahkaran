using AbrPlus.Integration.OpenERP.DI;
using AbrPlus.Integration.OpenERP.Hosting.DI;
using AbrPlus.Integration.OpenERP.Hosting.Hosting;
using AbrPlus.Integration.OpenERP.SampleERP.Api.DI;
using AbrPlus.Integration.OpenERP.SampleERP.Options;
using AbrPlus.Integration.OpenERP.SampleERP.Repository.DI;
using AbrPlus.Integration.OpenERP.SampleERP.Service.DI;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeptaKit.DI;
using System.Linq;

namespace AbrPlus.Integration.OpenERP.SampleERP.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.GeneralConfigure(Configuration);

            services.Configure<RahkaranUrlInfo>(x => Configuration.GetSection("Rahkaran").Bind(x));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var baseUrl = Configuration.GetValue<string>("RahkaranUrlOption:BaseUrl");

            builder.RegisterModule<InfrastructureDIModule>();
            builder.RegisterModule<ExtendedDIModule>();
            builder.RegisterModule<RepositoryDIModule>();
            builder.RegisterModule(new ServiceDIModule(baseUrl?.StartsWith("https") ?? false));
            builder.RegisterModule<ApiDIModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

            app.UseEndpoints(endpoints =>
            {
                var asembely = typeof(Startup).Assembly;
                var apiTypes = asembely.GetTypes().Where(x => typeof(IApi).IsAssignableFrom(x) && !x.IsInterface).ToList();
                foreach (var apiType in apiTypes)
                {
                    endpoints.UseSoapEndPoint(apiType);
                    endpoints.UseGrpcEndPoint(apiType);
                }
            });
        }
    }
}
