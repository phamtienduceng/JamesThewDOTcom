using Microsoft.AspNetCore.Builder;

namespace JamesRecipes
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // ...
            services.AddHttpClient("MyHttpClient", client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);

                // Ví dụ: Cấu hình Headers
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                // Ví dụ: Cấu hình BaseAddress
                client.BaseAddress = new Uri("https://api.example.com/");
            });
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

