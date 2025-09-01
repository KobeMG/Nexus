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
            var products = await _context.Products.ToListAsync();
            return View(products); //Enviar los productos de la BD a la vista!
            //return View(products);
        }

        public async Task<IActionResult> ProductDetails(int id) //Hace llamado a la vista de productos.
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Retorna un error 404 si el producto no se encuentra
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
