using LitLabKata.Models;

namespace LitLabKata.Services
{
    public interface IUserManagementService
    {
        List<User> AddUser(User user);
        List<User> DeleteUser(string nick);
        List<User> GetAllUsers();
        User GetUserByNick(string nick);
        List<User> UpdateUser(string nick, User user);
    }
}