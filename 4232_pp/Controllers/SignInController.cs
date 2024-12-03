using Microsoft.AspNetCore.Mvc;
using _4232_pp.Models;
using _4232_pp.Repositories;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace _4232_pp.Controllers
{
    public class SignInController : Controller
    {
        private readonly AptekaContext _context;

        public SignInController(AptekaContext context)
        {
            _context = context;
        }

        public IActionResult SignInPage()
        {
            return View("~/Views/SignIn.cshtml");
        }

        [HttpPost]
        public IActionResult SignIn(string Email, string Password)
        {
            UserRepositoriesImpl userRepo = new UserRepositoriesImpl(_context);
            User user = userRepo.GetUserByEmail(Email);

            if (user != null)
            {
                string hashedPassword = GetHashString(Password);

                if (hashedPassword == user.Password)
                {
                    // Сохраняем UserId в сессии
                    HttpContext.Session.SetString("UserId", user.UserId.ToString());

                    // Если у пользователя есть корзина, сохраняем ее Id в сессии
                    if (user.Cart != null)
                    {
                        HttpContext.Session.SetString("CartId", user.Cart.CartId.ToString());
                    }

                    return View("~/Views/Profile.cshtml", user);
                }
            }

            return View("~/Views/SignIn.cshtml");
        }

        private string GetHashString(string s)
        {
            using (MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider())
            {
                byte[] bytes = Encoding.Unicode.GetBytes(s);
                byte[] byteHash = CSP.ComputeHash(bytes);
                StringBuilder hashBuilder = new StringBuilder();
                foreach (byte b in byteHash)
                {
                    hashBuilder.Append(b.ToString("x2"));
                }
                return hashBuilder.ToString();
            }
        }
    }
}
