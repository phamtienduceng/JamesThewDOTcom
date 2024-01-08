using JamesRecipes.Models.Book;
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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<JamesRecipes.Models.Book.Cart>();

            services.AddHttpContextAccessor();

            services.AddScoped<Cart>(sp => Cart.GetCart(sp));

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                //options.IdleTimeout = TimeSpan.FromSeconds(10);
            });

            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSession();

        }

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    //Initialize Roles and Users. This one will create a user with an Admin - role aswell as a separate User-role.See UserRoleInitializer.cs in Models
                    //UserRoleInitializer.InitializeAsync(services).Wait();
                    //Seed book data
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured while attempting to seed the database");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

