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
                services.AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn của Session
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });
            });

            // ...
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSession();

        }
    }
}

