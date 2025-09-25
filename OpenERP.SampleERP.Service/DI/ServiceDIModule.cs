using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using Autofac;
using SeptaKit.Autofac;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.DI;

public class ServiceDIModule(bool useSsl) : BaseDIModule<ServiceDIModule>
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        if (useSsl)
        {
            builder.RegisterType<AuthenticationSslService>()
                .As<IAuthenticationService>()
                .InstancePerLifetimeScope();
        }
        else
        {
            builder.RegisterType<AuthenticationHttpService>()
                .As<IAuthenticationService>()
                .InstancePerLifetimeScope();
        }

        builder.RegisterType<TokenService>()
           .As<ITokenService>()
           .SingleInstance();
    }
}
