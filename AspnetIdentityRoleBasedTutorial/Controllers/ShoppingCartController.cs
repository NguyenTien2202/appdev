using AspnetIdentityRoleBasedTutorial.Data;
using AspnetIdentityRoleBasedTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AspnetIdentityRoleBasedTutorial.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = User.Identity.Name;
            var cartItems = _context.ShoppingCartItems.Include(item => item.Jewelry)
                                .Where(item => item.UserId == userId)
                                .ToList();

            return View(cartItems);
        }


        [HttpPost]
        public IActionResult AddToCart(int jewelryId, int quantity)
        {
            if (User.Identity.IsAuthenticated)
            {
                var existingCartItem = _context.ShoppingCartItems
                    .FirstOrDefault(item => item.JewelryId == jewelryId && item.UserId == User.Identity.Name);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += quantity;
                }
                else
                {
                    var cartItem = new ShoppingCartItem
                    {
                        JewelryId = jewelryId,
                        Quantity = quantity,
                        UserId = User.Identity.Name,
                    };
                    _context.ShoppingCartItems.Add(cartItem);
                }

                _context.SaveChanges();

               return RedirectToAction("Index");
            }
            else
            {
                return Redirect("/Identity/Account/Register");
            }

        }



        [HttpPost]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            var cartItem = _context.ShoppingCartItems.Find(cartItemId);

            if (cartItem != null)
            {
                _context.ShoppingCartItems.Remove(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCart(int cartItemId, int quantity)
        {
            var cartItem = _context.ShoppingCartItems.Find(cartItemId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CalculateTotalAndRefreshCart()
        {
            var userId = User.Identity.Name;
            var cartItems = _context.ShoppingCartItems.Include(item => item.Jewelry)
                                .Where(item => item.UserId == userId)
                                .ToList();
            decimal total = cartItems.Sum(item => (decimal)item.Quantity * (decimal)item.Jewelry.Price);

            ViewBag.Total = total.ToString("N0");

            return View("Index", cartItems);
        }
         public IActionResult GetCartCount()
       {
           var userId = User.Identity.Name;
           var cartItemCount = _context.ShoppingCartItems
               .Where(item => item.UserId == userId)
               .Sum(item => item.Quantity);

           return Json(cartItemCount);
       }

    }

}
