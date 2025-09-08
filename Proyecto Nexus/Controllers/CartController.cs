using Microsoft.AspNetCore.Mvc;
using Proyecto_Nexus.Models;
using Proyecto_Nexus.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Nexus.Controllers
{
    public class CartController : Controller
    {
        private readonly StoreDbContext _context;

        public CartController(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() //Se usa los ViewModels CartViewModel y CartItemViewModel, para evitar pasar entidades de la base de datos a la vista
        {
            var shoppingCartId = GetShoppingCartId();

            var cartItems = await _context.CartItems
                                          .Where(ci => ci.ShoppingCartId == shoppingCartId)
                                          .Include(ci => ci.Product)
                                              .ThenInclude(p => p.Images)
                                          .ToListAsync();

            // Mapear los modelos de la base de datos a un View Model
            var cartViewModel = new CartViewModel();
            cartViewModel.CartItems = new List<CartItemViewModel>();

            decimal grandTotal = 0;

            foreach (var item in cartItems)
            {
                var product = item.Product;
                var imageUrl = product?.Images?.FirstOrDefault()?.ImageUrl ?? "";
                var totalItemPrice = item.Quantity * product.Price;
                grandTotal += totalItemPrice;

                cartViewModel.CartItems.Add(new CartItemViewModel
                {
                    ProductName = product.Name,
                    ImageUrl = imageUrl,
                    Quantity = item.Quantity,
                    Price = product.Price,
                    TotalItemPrice = totalItemPrice
                });
            }

            cartViewModel.GrandTotal = grandTotal;

            return View(cartViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            // 1. Obtener el carrito de compras del usuario (si existe)
            var shoppingCartId = GetShoppingCartId();
            var shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(c => c.Id == shoppingCartId);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart { Id = shoppingCartId, CreatedDate = DateTime.Now }; //Se crea si no existe
                _context.ShoppingCarts.Add(shoppingCart);
                await _context.SaveChangesAsync();
            }

            // 2. Verificar si el producto ya está en el carrito
            var cartItem = await _context.CartItems
                                         .Include(ci => ci.Product)
                                         .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.ShoppingCartId == shoppingCart.Id);

            if (cartItem == null)
            {
                // El producto no está en el carrito, se añade un nuevo ítem
                cartItem = new CartItem
                {
                    ProductId = productId,
                    ShoppingCartId = shoppingCart.Id,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                // El producto ya está en el carrito, se actualiza la cantidad
                cartItem.Quantity += quantity;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home"); // Redirige a la página principal
        }


        private string GetShoppingCartId() // Esta funcion luego debe ser movida a un servicio de carrito de compras TODO
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ShoppingCartId")))
            {
                HttpContext.Session.SetString("ShoppingCartId", Guid.NewGuid().ToString());
            }

            return HttpContext.Session.GetString("ShoppingCartId");
        }
    }
}
