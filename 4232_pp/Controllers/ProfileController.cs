using _4232_pp.Models;
using _4232_pp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace _4232_pp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AptekaContext _context;

        public ProfileController(AptekaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId))
            {
                UserRepositoriesImpl userRepo = new UserRepositoriesImpl(_context);
                User user = userRepo.GetUserById(long.Parse(userId));
                if (user != null)
                {
                    // Если у пользователя есть корзина, сохраняем ее Id в сессии
                    if (user.Cart != null)
                    {
                        HttpContext.Session.SetString("CartId", user.Cart.CartId.ToString());
                    }
                    return View("~/Views/Profile.cshtml", user);
                }
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public IActionResult DeleteAccount(long userId)
        {
            UserRepositoriesImpl userRepo = new UserRepositoriesImpl(_context);
            User user = userRepo.GetUserById(userId);
            if (user != null)
            {
                userRepo.DeleteUser(user);

                // Очищаем сессию, чтобы убрать информацию о текущем пользователе
                HttpContext.Session.Clear();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public IActionResult UpdateAccount(long userId, string newName = null, string newEmail = null, string newPassword = null)
        {
            UserRepositoriesImpl userRepo = new UserRepositoriesImpl(_context);
            User user = userRepo.GetUserById(userId);
            if (user != null)
            {
                if (newName != null)
                {
                    user.Name = newName;
                }
                if (newEmail != null)
                {
                    user.Email = newEmail;
                }
                if (newPassword != null)
                {
                    user.Password = GetHashString(newPassword);
                }
                userRepo.UpdateUser(user);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Error", "Home");
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
