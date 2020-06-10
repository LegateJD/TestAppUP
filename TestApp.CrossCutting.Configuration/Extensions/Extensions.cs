using TestApp.Dal;
using Microsoft.AspNetCore.Builder;

namespace TestApp.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static void UpdateDatabase(
            this IApplicationBuilder app)
        {
            app.ApplicationServices.GetService(typeof(TestAppContext));
        }
    }
}
