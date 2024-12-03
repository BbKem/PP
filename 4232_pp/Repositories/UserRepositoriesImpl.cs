using Microsoft.AspNetCore.Mvc;
using _4232_pp.Models;

namespace _4232_pp.Repositories
{
    public class UserRepositoriesImpl : UserRepositories
    {
        private AptekaContext _context;
        public UserRepositoriesImpl(AptekaContext context)
        {
            this._context = context;
        }
        public void SaveUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            User User = new User();
            try
            {
                User = _context.Users.FirstOrDefault(x => x.Email == email);

            }
            catch (ArgumentNullException ex)
            {

            }
            return User;
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User updatedUser)
        {
            var existingUser = _context.Users.Find(updatedUser.UserId);
            if (existingUser != null)
            {
                existingUser.Name = updatedUser.Name;
                existingUser.Email = updatedUser.Email;
                existingUser.Password = updatedUser.Password; 
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("");
            }
        }

        public User GetUserById(long userId)
        {
            User user = new User();
            try
            {
                user = _context.Users.FirstOrDefault(x => x.UserId == userId);
            }
            catch (ArgumentNullException ex)
            {

            }
            return user;
        }

    }
}
