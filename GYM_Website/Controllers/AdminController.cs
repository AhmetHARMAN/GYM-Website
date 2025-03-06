using Microsoft.AspNetCore.Mvc;

using spor_salonu.DbContexts;

using Microsoft.AspNetCore.Authorization;

namespace spor_salonu.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        

        public AdminController(AppDbContext context)
        {
            _context = context;
          
        }

        

        


    [Authorize]

        public IActionResult UserList()
        {  
            
            var users = _context.Users.ToList();
            if (users == null || !users.Any())
            {
                // Veritabanında kullanıcı yoksa, hata mesajı veya başka bir işlem yapılabilir.
                return View("Error");
            }           
            return View(users);
            

        }
        [Authorize]


        public IActionResult AdminList()
        {
            var admins = _context.Admins.ToList();
            return View(admins);
        }

        [Authorize]

        public IActionResult Mesajlar()
        {
            var mesajs = _context.Mesajlar.ToList();
            return View(mesajs);
        }
    }
}
