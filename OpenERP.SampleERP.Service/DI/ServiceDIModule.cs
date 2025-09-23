using AbrPlus.Integration.OpenERP.SampleERP.Service.LoginServices;
using Autofac;
using SeptaKit.Autofac;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.DI;

public class ServiceDIModule(bool useSlsl) : BaseDIModule<ServiceDIModule>
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        if (useSlsl)
        {
            builder.RegisterType<RahkaranAuthenticationSslService>()
                .As<IRahkaranAuthenticationService>()
                .InstancePerLifetimeScope();
        }
        else
        {
            builder.RegisterType<RahkaranAuthenticationHttpService>()
                .As<IRahkaranAuthenticationService>()
                .InstancePerLifetimeScope();
        }

        builder.RegisterType<RahkaranSessionService>()
           .As<IRahkaranSessionService>()
           .SingleInstance();
    }
}
