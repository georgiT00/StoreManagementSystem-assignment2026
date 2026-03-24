namespace StoreManagementSystem.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    using Data.SeedData.Interfaces;

    public static class WebAppExtensions
    {
        public static IApplicationBuilder UseRoleSeed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                IIdentityRoleSeed roleSeeder = scope.ServiceProvider
                    .GetRequiredService<IIdentityRoleSeed>();

                roleSeeder
                    .SeedRolesAsync()
                    .GetAwaiter()
                    .GetResult();
            }
            return app;
        }
    }
}
