using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spor_salonu.DbContexts;
using spor_salonu.Models;
using System.Diagnostics;

namespace spor_salonu.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		
        private readonly AppDbContext _context; // Veritabaný baðlamý

       

        public HomeController(AppDbContext context ,ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        

        public IActionResult Hakkýmýzda()
		{
			return View();
		}
		public IActionResult Anasayfa()
		{
			return View();
		}
		
        public IActionResult Hizmetlerimiz()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpGet]
        public IActionResult Ýletiþim()
        {
            return View();
        }
		[HttpPost]
		public IActionResult Ýletiþim(Mesaj model)
		{


            try
            {
                Console.WriteLine("Kullanýcý ekleme iþlemi baþladý.");
                Console.WriteLine($"Kullanýcý Adý: {model.Ýsim}, Email: {model.Mail}");

                _context.Mesajlar.Add(model);
                Console.WriteLine("Kullanýcý veritabanýna eklendi. SaveChanges çalýþtýrýlýyor...");

                var result = _context.SaveChanges();
                Console.WriteLine($"SaveChanges sonucu: {result}");

                if (result > 0)
                {
                    Console.WriteLine("Kullanýcý baþarýyla kaydedildi!");
                    return RedirectToAction("Anasayfa", "Home");
                }
                else
                {
                    Console.WriteLine("Kayýt iþlemi tamamlanamadý.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluþtu: {ex.Message}");
            }
            return View(model);
        }
        
    }
}
