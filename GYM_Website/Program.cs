using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using spor_salonu.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Servisleri yapýlandýr
builder.Services.AddControllersWithViews(); // MVC desteði eklenir

builder.Services.AddAuthorization(); // Yetkilendirme eklenir (isteðe baðlý)

builder.Services.AddAuthentication("CookieAuth") // Kimlik doðrulama eklenir (isteðe baðlý)
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Account/Admin";
        options.LoginPath = "/Account/Admin";
        options.ExpireTimeSpan = TimeSpan.FromSeconds(60);
        options.AccessDeniedPath = "/Account/AccessDenied";
    });


builder.Services.AddMemoryCache();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


static void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();
    //services.AddSession(options =>
    //{
    //    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session süresi
    //    options.Cookie.HttpOnly = true;
    //    options.Cookie.IsEssential = true;
    //});
}

var app = builder.Build();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax,
    Secure = CookieSecurePolicy.None
});
// Ortam ayarlarýna göre middleware yapýlandýr
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// HTTP middleware'lerini yapýlandýr
app.UseHttpsRedirection(); // HTTPS yönlendirmesi
app.UseStaticFiles(); // Statik dosyalar için destek

app.UseRouting(); // Rota iþlemleri için middleware
app.UseAuthentication(); // Kimlik doðrulama
app.UseAuthorization(); // Yetkilendirme

// Varsayýlan rota yapýlandýrmasý
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Anasayfa}/{id?}");

app.Run();


