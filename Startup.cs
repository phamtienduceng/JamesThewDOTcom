using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

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
                    options.Cookie.Name = "YourSessionCookieName";
                    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn của Session
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });
            });

            services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        options.ClientId = "1020413070577-hrkbsnc1de3qje8etb3jpfjj2hr30ou3.apps.googleusercontent.com";
                        options.ClientSecret = "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";
                    });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}

