using Autofac;
using AutoMapper;
using TestApp.Infrastructure.Configuration.Autofac;
using TestApp.WEB.Infrastructure.Interfaces;
using TestApp.WEB.Infrastructure.Managers;

namespace TestApp.WEB.Configuration
{
    public class WebAutofacModule : InfrastuctureAutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(cfg => new MapperConfiguration(c => c.AddProfile<WebMapperProfile>()).CreateMapper())
                .As<IMapper>().SingleInstance();
            builder.RegisterType<CookieBasketManager>().As<IBasketManager>();
        }
    }
}
