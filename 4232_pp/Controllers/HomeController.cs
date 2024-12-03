using _4232_pp.Models;
using _4232_pp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _4232_pp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AptekaContext _context;

        public HomeController(AptekaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.UserId = HttpContext.Session.GetString("UserId");
            return View();
        }

        public IActionResult AptekaBD()
        {
            ViewBag.UserId = HttpContext.Session.GetString("UserId");
            return View(_context.Products.ToList());
        }


        public IActionResult Profile()
        {
            long userId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            User user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Cart()
        {
            string cartIdString = HttpContext.Session.GetString("CartId");

            if (!string.IsNullOrEmpty(cartIdString) && long.TryParse(cartIdString, out long cartId))
            {
                var cartItems = _context.CartItems
                    .Include(ci => ci.Product)
                    .Where(ci => ci.CartId == cartId)
                    .ToList();


                return View(cartItems);
            }
            else
            {
                return View(new List<CartItem>());
            }
        }

        [HttpPost]
        public IActionResult RemoveFromCart(long cartItemId)
        {
            long userId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            User user = _context.Users.Include(u => u.Cart).ThenInclude(c => c.CartItems).FirstOrDefault(u => u.UserId == userId);

            if (user != null)
            {
                CartItem cartItemToRemove = user.Cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
                if (cartItemToRemove != null)
                {
                    _context.CartItems.Remove(cartItemToRemove);
                    user.Cart.CartItems.Remove(cartItemToRemove);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Cart");
        }

        public IActionResult AddToCart(long productId)
        {
            long userId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            User user = _context.Users.Include(u => u.Cart).ThenInclude(c => c.CartItems).FirstOrDefault(u => u.UserId == userId);

            Product product = _context.Products.Find(productId);

            if (user != null && product != null)
            {
                if (user.Cart == null)
                {
                    user.Cart = new Cart();
                    user.Cart.UserId = userId;
                    user.Cart.CartItems = new List<CartItem>();
                    _context.Carts.Add(user.Cart);
                }

                CartItem existingCartItem = user.Cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += 1;
                }
                else
                {
                    CartItem newCartItem = new CartItem
                    {
                        CartId = user.Cart.CartId,
                        ProductId = productId,
                        Quantity = 1
                    };
                    _context.CartItems.Add(newCartItem);
                    user.Cart.CartItems.Add(newCartItem); 
                }

                _context.SaveChanges();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("CartId")))
                {
                    HttpContext.Session.SetString("CartId", user.Cart.CartId.ToString());
                }
            }

            return RedirectToAction("AptekaBD");
        }

        public IActionResult PlaceOrder()
        {
            string userIdString = HttpContext.Session.GetString("UserId");

            if (!string.IsNullOrEmpty(userIdString) && long.TryParse(userIdString, out long userId))
            {
                var user = _context.Users.Include(u => u.Cart).ThenInclude(c => c.CartItems).FirstOrDefault(u => u.UserId == userId);

                if (user != null && user.Cart != null && user.Cart.CartItems.Any())
                {
                    Order newOrder = new Order
                    {
                        UserId = user.UserId,
                        OrderDate = DateTime.Now,
                    };
                    _context.Orders.Add(newOrder);
                    _context.SaveChanges(); 

                    foreach (var cartItem in user.Cart.CartItems)
                    {
                        OrderDetail orderDetail = new OrderDetail
                        {
                            OrderId = newOrder.OrderId, 
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity,

                        };
                        _context.OrderDetails.Add(orderDetail);
                    }

                    _context.CartItems.RemoveRange(user.Cart.CartItems);
                    _context.SaveChanges();

                    return RedirectToAction("Orders", "Home");
                }
            }

            return RedirectToAction("Cart", "Home");
        }

        public IActionResult Orders()
        {
            string userIdString = HttpContext.Session.GetString("UserId");

            if (!string.IsNullOrEmpty(userIdString) && long.TryParse(userIdString, out long userId))
            {
                var orders = _context.Orders
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                    .Where(o => o.UserId == userId)
                    .ToList();

                foreach (var order in orders)
                {
                    foreach (var orderDetail in order.OrderDetails)
                    {
                        orderDetail.Price = orderDetail.Quantity * orderDetail.Product.Price;
                    }
                }

                return View(orders);
            }

            return RedirectToAction("Index", "Home");
        }


    }
}
