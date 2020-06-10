using Autofac;
using TestApp.Infrastructure.Extensions;
using TestApp.WEB.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stripe;

namespace TestApp.WEB
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
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
            app.UseDeveloperExceptionPage();
            loggerFactory.AddFile("Logs/myApp.txt");
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Products}/{action=GetAll}/{id?}");
            });

            app.UpdateDatabase();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new WebAutofacModule());
        }
    }
}
