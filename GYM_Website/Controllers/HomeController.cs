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

		
        private readonly AppDbContext _context; // Veritaban� ba�lam�

       

        public HomeController(AppDbContext context ,ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        

        public IActionResult Hakk�m�zda()
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
        public IActionResult �leti�im()
        {
            return View();
        }
		[HttpPost]
		public IActionResult �leti�im(Mesaj model)
		{


            try
            {
                Console.WriteLine("Kullan�c� ekleme i�lemi ba�lad�.");
                Console.WriteLine($"Kullan�c� Ad�: {model.�sim}, Email: {model.Mail}");

                _context.Mesajlar.Add(model);
                Console.WriteLine("Kullan�c� veritaban�na eklendi. SaveChanges �al��t�r�l�yor...");

                var result = _context.SaveChanges();
                Console.WriteLine($"SaveChanges sonucu: {result}");

                if (result > 0)
                {
                    Console.WriteLine("Kullan�c� ba�ar�yla kaydedildi!");
                    return RedirectToAction("Anasayfa", "Home");
                }
                else
                {
                    Console.WriteLine("Kay�t i�lemi tamamlanamad�.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata olu�tu: {ex.Message}");
            }
            return View(model);
        }
        
    }
}
