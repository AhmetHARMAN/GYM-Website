using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using spor_salonu.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Servisleri yap�land�r
builder.Services.AddControllersWithViews(); // MVC deste�i eklenir

builder.Services.AddAuthorization(); // Yetkilendirme eklenir (iste�e ba�l�)

builder.Services.AddAuthentication("CookieAuth") // Kimlik do�rulama eklenir (iste�e ba�l�)
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
    //    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session s�resi
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
// Ortam ayarlar�na g�re middleware yap�land�r
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// HTTP middleware'lerini yap�land�r
app.UseHttpsRedirection(); // HTTPS y�nlendirmesi
app.UseStaticFiles(); // Statik dosyalar i�in destek

app.UseRouting(); // Rota i�lemleri i�in middleware
app.UseAuthentication(); // Kimlik do�rulama
app.UseAuthorization(); // Yetkilendirme

// Varsay�lan rota yap�land�rmas�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Anasayfa}/{id?}");

app.Run();


