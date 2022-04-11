using LitLabKata.Helpers;
using LitLabKata.Models;
using LitLabKata.Repositories;

namespace LitLabKata.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger logger;

        public UserManagementService(IUserRepository userRepository, ILogger<IUserManagementService> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public List<User> AddUser(User user)
        {
            try
            {
                ValidateUser(user);
                var result = userRepository.AddUser(user);

                logger.LogInformation($"The user { user.Nick } was created.");

                return result;
            }
            catch (ArgumentException e)
            {
                var exception = new UserAlreadyExistsException($"The nick { user.Nick } is already in use. Please choose a different one.", e);
                logger.LogError($"A user with the nick { user.Nick } was tried to be created, but it is a repeated nick", exception);

                throw exception;
            }
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public User GetUserByNick(string nick)
        {
            try
            {
                return userRepository.GetUserByNick(nick);
            }
            catch (KeyNotFoundException e)
            {
                logger.LogInformation($"User { nick } was not found.", e);
                return null;
            }
        }

        public List<User> DeleteUser(string nick)
        {
            try
            {
                var result = userRepository.DeleteUser(nick);

                logger.LogInformation($"The user { nick } was deleted.");

                return result;
            }
            catch (KeyNotFoundException e)
            {
                logger.LogError($"The user {nick} does not exist, cannot be deleted.", e);
                return userRepository.GetAllUsers();
            }
        }

        public List<User> UpdateUser(string nick, User user)
        {
            try
            {
                ValidateUser(user);
                var result = userRepository.UpdateUser(nick, user);

                logger.LogInformation($"The user { user.Nick } was updated.");

                return result;
            }
            catch (KeyNotFoundException e)
            {
                logger.LogError($"The user {nick} does not exist, cannot be updated.", e);
                return userRepository.GetAllUsers();
            }
        }

        private void ValidateUser(User user)
        {
            try
            {
                user.Validate();
            }
            catch (UserMissingFieldsException e)
            {
                logger.LogError($"The used has some missing information.", e);
                throw e;
            }
            catch (InvalidUserPhoneException e)
            {
                logger.LogError($"The phone { user.Phone } for user { user.Nick } is not formatted properly.", e);
                throw e;
            }
            catch (InvalidUserEmailException e)
            {
                logger.LogError($"The email { user.Email } for user { user.Nick } is not formatted properly.", e);
                throw e;
            }
        }
    }
}