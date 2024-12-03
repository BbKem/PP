using _4232_pp.Models;

namespace _4232_pp.Repositories
{
    public interface UserRepositories
    {
        User GetUserByEmail(string email);
        User GetUserById(long userId);
        void SaveUser(User user);
        void UpdateUser(User updatedUser);
        void DeleteUser(User user);
    }
}
