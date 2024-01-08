using JamesRecipes.Service.Admin;
using JamesRecipes.Service.FE;
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

            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
                cfg.Cookie.Name = "Jamesrecipe";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                cfg.IdleTimeout = new TimeSpan(0, 30, 0);    // Thời gian tồn tại của Session
            });

            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            services.AddTransient<CartService>();
            services.AddScoped<JamesRecipes.Service.FE.CartService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSession();

        }
    }
}

