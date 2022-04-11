using LitLabKata.Helpers;
using LitLabKata.Models;
using LitLabKata.Repositories;
using LitLabKata.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace LitLabKata.Test.Services
{
    public class UserManagementServiceShould
    {
        private IUserRepository userRepo;
        private Mock<ILogger<IUserManagementService>> logger;

        [SetUp]
        public void Setup()
        {
            userRepo = new UserRepository();
            logger = new Mock<ILogger<IUserManagementService>>();
        }

        [Test]
        public void ReturnListWithNewlyAddedUserAndPreviousOnes()
        {
            var userManagementService = new UserManagementService(userRepo, logger.Object);
            var testUser = new User("testuser", "test", "user", "testUser@gmail.com", "test road, n42", "123456789");

            var result = userManagementService.AddUser(testUser);

            Assert.IsTrue(result.Contains(testUser));
        }

        [Test]
        public void ThrowExceptionWhenTwoUsersAreAddedWithTheSameNick()
        {
            var userManagementService = new UserManagementService(userRepo, logger.Object);
            var testUser = new User("testuser", "test", "user", "testUser@gmail.com", "test road, n42", "123456789");

            _ = userManagementService.AddUser(testUser);

            Assert.Throws<UserAlreadyExistsException>(() => userManagementService.AddUser(testUser));
        }

        [Test]
        public void ReturnUserSearchedByNick()
        {
            var userManagementService = new UserManagementService(userRepo, logger.Object);
            var testUser = new User("testuser", "test", "user", "testUser@gmail.com", "test road, n42", "123456789");
            var testUserToFind = new User("testuser2", "test2", "user2", "testUser2@gmail.com", "test road, n42B", "123456788");

            _ = userManagementService.AddUser(testUser);
            _ = userManagementService.AddUser(testUserToFind);

            var result = userManagementService.GetUserByNick("testuser2");

            Assert.AreEqual(testUserToFind, result);
        }

        [Test]
        public void ReturnNullIfNoUserWasFound()
        {
            var userManagementService = new UserManagementService(userRepo, logger.Object);
            var testUser = new User("testuser", "test", "user", "testUser@gmail.com", "test road, n42", "123456789");

            _ = userManagementService.AddUser(testUser);

            var result = userManagementService.GetUserByNick("testuser2");

            Assert.IsNull(result);
        }

        [Test]
        public void ReturnListWithoutTheDeletedUser()
        {
            var userManagementService = new UserManagementService(userRepo, logger.Object);
            var testUser = new User("testuser", "test", "user", "testUser@gmail.com", "test road, n42", "123456789");
            var testUserToDelete = new User("testuser2", "test2", "user2", "testUser2@gmail.com", "test road, n42B", "123456788");

            _ = userManagementService.AddUser(testUser);
            _ = userManagementService.AddUser(testUserToDelete);

            var result = userManagementService.DeleteUser("testuser2");

            Assert.IsFalse(result.Contains(testUserToDelete));
        }

        [Test]
        public void ReturnTheSameListIfTheUserToDeleteDoesNotExist()
        {
            var userManagementService = new UserManagementService(userRepo, logger.Object);
            var testUser = new User("testuser", "test", "user", "testUser@gmail.com", "test road, n42", "123456789");

            _ = userManagementService.AddUser(testUser);

            var result = userManagementService.DeleteUser("testuser2");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(testUser, result[0]);
        }

        [Test]
        public void ReturnListWithUpdatedUser()
        {
            var userManagementService = new UserManagementService(userRepo, logger.Object);
            var testUser = new User("testuser", "test", "user", "testUser@gmail.com", "test road, n42", "123456789");
            var testUserUpdated = new User("testuser", "test2", "user", "testUser@gmail.com", "test road, n42", "123456789");

            _ = userManagementService.AddUser(testUser);

            var result = userManagementService.UpdateUser(testUser.Nick, testUserUpdated);

            Assert.AreEqual(testUserUpdated, result[0]);
        }

        [Test]
        public void ReturnNullIfTheUpdatedUserDoesNotExist()
        {
            var userManagementService = new UserManagementService(userRepo, logger.Object);
            var testUser = new User("testuser", "test", "user", "testUser@gmail.com", "test road, n42", "123456789");
            var testUserUpdated = new User("inexisting", "test2", "user", "testUser@gmail.com", "test road, n42", "123456789");

            _ = userManagementService.AddUser(testUser);

            var result = userManagementService.UpdateUser(testUserUpdated.Nick, testUserUpdated);

            Assert.IsNull(result);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var user in userRepo.GetAllUsers())
            {
                userRepo.DeleteUser(user.Nick);
            }
        }
    }
}