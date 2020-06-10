using Autofac;
using TestApp.Dal;
using TestApp.Dal.Repositories;
using TestApp.Dal.UnitsOfWork;
using TestApp.Domain.Interfaces;
using TestApp.Domain.Interfaces.Repositories;
using TestApp.Domain.Interfaces.UnitsOfWork;
using TestApp.Domain.Models;
using TestApp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestApp.Infrastructure.Configuration.Autofac
{
    public class InfrastuctureAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                var opt = new DbContextOptionsBuilder<TestAppContext>();
                var str = config.GetConnectionString("DefaultConnection");
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                return new TestAppContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().As<IRepository<Product>>();
            builder.RegisterType<OrderRepository>().As<IRepository<Order>>();
            builder.RegisterType<OrderDetailsRepository>().As<IRepository<OrderDetails>>();
        }
    }
}
