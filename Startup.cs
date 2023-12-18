using Microsoft.AspNetCore.Builder;

namespace JamesRecipes
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // ...

            services.AddSession(options =>
            {
                // Thiết lập các tùy chọn session
                options.Cookie.Name = "YourSessionCookieName";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            // ...
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSession();

        }



    }
}

