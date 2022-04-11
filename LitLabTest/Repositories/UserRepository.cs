using LitLabKata.Models;

namespace LitLabKata.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly Dictionary<string, User> userStorage = new();

        public UserRepository()
        {
        }

        public List<User> AddUser(User user)
        {
            userStorage.Add(user.Nick, user);

            return GetAllUsers();
        }

        public List<User> GetAllUsers()
        {
            return userStorage.Values.ToList();
        }

        public User GetUserByNick(string nick)
        {
            return userStorage[nick];
        }

        public List<User> DeleteUser(string nick)
        {
            userStorage.Remove(nick);

            return GetAllUsers();
        }

        public List<User> UpdateUser(string nick, User user)
        {
            var userToUpdate = GetUserByNick(nick);

            userStorage[userToUpdate.Nick] = user;

            return GetAllUsers();
        }
    }
}
