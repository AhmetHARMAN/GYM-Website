using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spor_salonu.DbContexts;
using spor_salonu.Models;
using System.Security.Claims;

namespace spor_salonu.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppDbContext _context; // Veritabanı bağlamı

        public AccountController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();


        }
        [HttpPost]

        public async Task<IActionResult> Login(string username, string password)
        {
            var users = _context.Users.FirstOrDefault(u => u.Kullanici_Ad == username);
          

            string hashPass = PasswordHelper.HashPassword(password);
            if (users != null)
            {
                if (users.Password == hashPass)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,users.Kullanici_Ad),
                    new Claim(ClaimTypes.Email,users.Email)
                };
                    var identity = new ClaimsIdentity(claims, "CookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("CookieAuth", principal);

                    return RedirectToAction("Anasayfa", "Home");
                }
            }
            ViewBag.Error = "Kullanıcı Adı veya Şifreniz Yanlış";

            return View();
        }



       

        [HttpGet]
        public IActionResult Admin()
        {
            return View();


        }
        [HttpPost]

        public async Task<IActionResult> Admin(string username, string password)
        {
            var users = _context.Admins.FirstOrDefault(u => u.Kullanici_Ad == username) ;            
        
            string hashPass = PasswordHelper.HashPassword(password);
            if (users != null)
            {
                if (users.Password == hashPass)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,users.Kullanici_Ad),
                    new Claim(ClaimTypes.Email,users.Email)
                };
                    var identity = new ClaimsIdentity(claims, "CookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("CookieAuth", principal);

                    return RedirectToAction("UserList", "Admin");
                }
            }
            ViewBag.Error = "Kullanıcı Adı veya Şifreniz Yanlış";
            return View();
        }

        public async Task<IActionResult> Logout2()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Admin");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine("Kullanıcı ekleme işlemi başladı.");
                    Console.WriteLine($"Kullanıcı Adı: {model.Kullanici_Ad}, Email: {model.Email}");
                    model.Password = PasswordHelper.HashPassword(model.Password);
                    _context.Users.Add(model);
                    Console.WriteLine("Kullanıcı veritabanına eklendi. SaveChanges çalıştırılıyor...");

                    var result = _context.SaveChanges();
                    Console.WriteLine($"SaveChanges sonucu: {result}");

                    if (result > 0)
                    {
                        Console.WriteLine("Kullanıcı başarıyla kaydedildi!");
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        Console.WriteLine("Kayıt işlemi tamamlanamadı.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata oluştu: {ex.Message}");
                }
            }
            return View(model);
        }

    } 

}


