using Microsoft.AspNetCore.Mvc;
using _4232_pp.Models;
using _4232_pp.Repositories;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http; 

namespace _4232_pp.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly AptekaContext _context;

        public RegistrationController(AptekaContext context)
        {
            _context = context;
        }

        public IActionResult GetReg()
        {
            return View("~/Views/Registration.cshtml");
        }

        [HttpPost]
        public IActionResult Registration(string Name, string Email, string Password)
        {
            UserRepositoriesImpl userRepo = new UserRepositoriesImpl(_context);
            User existingUser = userRepo.GetUserByEmail(Email);
            if (existingUser != null)
            {
                return View("~/Views/Registration.cshtml");
            }

            User newUser = new User
            {
                Name = Name,
                Email = Email,
                Password = GetHashString(Password)
            };

            userRepo.SaveUser(newUser);

            // Добавим информацию о пользователе в HttpContext после успешной регистрации
            HttpContext.Session.SetString("UserId", newUser.UserId.ToString());

            return View("~/Views/Profile.cshtml", newUser);
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
