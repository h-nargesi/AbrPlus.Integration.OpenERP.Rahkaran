using AbrPlus.Integration.OpenERP.Hosting.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeptaKit.Extensions;
using SeptaKit.Hosting.AspNetCore;
using SeptaKit.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace AbrPlus.Integration.OpenERP.SampleERP.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => AspNetCoreHostBuilder.CreateHostBuilder<Startup>(args, options =>
        {
            options.Platform = RuntimePlatform.Windows;
            options.GetHttpPort = GetHttpPort;
            options.GetHttpsPort = GetHttpsPort;
            options.GetCertificate = GetCertificate;
            options.GetAppConfigSettings = GetAppConfigSettings<Program>;
            options.HostingMode = RuntimeHostingMode.Kestrel;
        });

        public static int GetHttpPort(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var port = configuration.GetValue<int>("App:HttpPort");
            return port;
        }

        public static int GetHttpsPort(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var port = configuration.GetValue<int>("App:HttpsPort");
            return port;
        }

        public static X509Certificate2 GetCertificate(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var authorityCert = configuration.GetValue<string>($"AuthorityCert");
            if (authorityCert.HasValue())
            {
                return CertificateLoader.LoadFromStoreCert(authorityCert, StoreName.Root.ToString(), StoreLocation.LocalMachine, false);
            }
            else
            {
                var env = serviceProvider.GetService<IHostEnvironment>();
                return new X509Certificate2(Path.Combine(env.ContentRootPath, Path.Combine("Certs", "localhost.pfx")), "13");
            }
        }

        public static List<AppConfigSetting> GetAppConfigSettings<T>()
        {
            var basePath = AppContext.BaseDirectory;

            List<AppConfigSetting> appConfigSettings = new List<AppConfigSetting>() {
                new AppConfigSetting
                {
                    Path=Path.Combine(basePath, "Configs", "appsettings.global.json"),
                    Optional=false,
                    ReloadOnChange=true
                },
                new AppConfigSetting
                {
                    Path=Path.Combine(basePath, "Configs", "appsettings.json"),
                    Optional=false,
                    ReloadOnChange=true
                }
            };
            return appConfigSettings;
        }
    }
}
