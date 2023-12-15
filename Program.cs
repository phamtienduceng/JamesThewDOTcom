using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JamesRecipes.Data;
using JamesRecipes.Repository.Admin;
using JamesRecipes.Repository.FE;
using JamesRecipes.Service.Admin;
using JamesRecipes.Service.FE;
using JamesRecipes.Models;

var builder = WebApplication.CreateBuilder(args);

// DI Admin

builder.Services.AddScoped<IAccountManagementRepository, AccountManagementService>();
builder.Services.AddScoped<IAnnounceManagementRepository, AnnouncementManagementService>();
builder.Services.AddScoped<IBookManagementRepository, BookManagementService>();
builder.Services.AddScoped<ICommentManagementRepository, CommentManagementService>();
builder.Services.AddScoped<IContactManagementRepository, ContactManagementService>();
builder.Services.AddScoped<IContestManagementRepository, ContestManagementService>();
builder.Services.AddScoped<IDashboardRepository, DashboardService>();
builder.Services.AddScoped<IFaqManagementRepository, FaqManagementService>();
builder.Services.AddScoped<IOrderManagementRepository, OrderManagementService>();
builder.Services.AddScoped<IRecipeManagementRepository, RecipeManagementService>();
builder.Services.AddScoped<ITipManagementRepository, TipManagementService>();

// DI Front-end
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAbout, AboutService>();
builder.Services.AddScoped<IAnnouncement, AnnouncementService>();
builder.Services.AddScoped<IBook, BookService>();
builder.Services.AddScoped<ICart, CartService>();
builder.Services.AddScoped<IContact, ContactService>();
builder.Services.AddScoped<IContest, ContestService>();
builder.Services.AddScoped<IFaq, FaqService>();
builder.Services.AddScoped<IRecipe, RecipeService>();
builder.Services.AddScoped<ITip, TipService>();

// Add services to the container.

builder.Services.AddSession();

builder.Services.AddDbContext<JamesrecipesContext>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "fe",
    pattern: "fe/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{controller=Dashboard}/{action=Index}/{id?}");

app.UseSession();

app.MapRazorPages();

app.Run();