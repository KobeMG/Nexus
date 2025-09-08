using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Nexus.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Proyecto_Nexus.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoreDbContext _context;

        public HomeController(StoreDbContext context) //Para hacer la intexión de la BD.
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Images).ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
