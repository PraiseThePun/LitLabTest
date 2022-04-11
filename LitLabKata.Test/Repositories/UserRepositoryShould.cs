using LitLabKata.Models;
using LitLabKata.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LitLabKata.Test.Repositories
{
    public class UserRepositoryShould
    {
        private IUserRepository userRepository;

        [SetUp]
        public void SetUp()
        {
            userRepository = new UserRepository();
        }

        [Test]
        public void ReturnCreatedUserWhenAddUserIsCalled()
        {
            var user = new User("nick", "name", "surname", "email", "direction", "123456789");
            var result = userRepository.AddUser(user);

            Assert.AreEqual(user, result[0]);
        }

        [Test]
        public void ThrowsExceptionWhenAddUserIsCalledAndAnotherUserExistsWithTheSameNick()
        {
            var user = new User("nick", "name", "surname", "email", "direction", "123456789");
            _ = userRepository.AddUser(user);

            Assert.Throws<ArgumentException>(() => userRepository.AddUser(user));
        }

        [Test]
        public void ReturnTheExpectedUserWhenGetUserByNickIsCalled()
        {
            var user = new User("nick", "name", "surname", "email", "direction", "123456789");
            _ = userRepository.AddUser(user);

            var result = userRepository.GetUserByNick(user.Nick);

            Assert.AreEqual(user, result);
        }

        [Test]
        public void ThrowsExceptionWhenGetUserByNickIsCalledAndUserDoesNotExist()
        {
            Assert.Throws<KeyNotFoundException>(() => userRepository.GetUserByNick("nouser"));
        }

        [Test]
        public void ReturnListWithoutDeletedUserWhenDeleteUserIsCalled()
        {
            var user = new User("nick", "name", "surname", "email", "direction", "123456789");
            var userToDelete = new User("nock", "name", "surname", "email", "direction", "123456789");
            _ = userRepository.AddUser(user);
            _ = userRepository.AddUser(userToDelete);

            var result = userRepository.DeleteUser(userToDelete.Nick);

            Assert.IsFalse(result.Contains(userToDelete));
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ReturnsSameListWhenDeleteIsCalledAndUserDoesNotExist()
        {
            var user = new User("nick", "name", "surname", "email", "direction", "123456789");
            _ = userRepository.AddUser(user);

            var result = userRepository.DeleteUser("nouser");

            Assert.AreEqual(user, result[0]);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ReturnListWithUpdatedUserWhenUpdateUserIsCalled()
        {
            var user = new User("nick", "name", "surname", "email", "direction", "123456789");
            var userToUpdate = new User("nick", "newName", "surname", "email", "direction", "123456789");
            _ = userRepository.AddUser(user);

            var result = userRepository.UpdateUser(userToUpdate.Nick, userToUpdate);

            Assert.AreEqual(userToUpdate, result[0]);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ThrowsExceptionWhenUpdateUserIsCalledAndUserDoesNotExsist()
        {
            Assert.Throws<KeyNotFoundException>(() => userRepository.UpdateUser("nouser", new User("nick", "newName", "surname", "email", "direction", "123456789")));
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var user in userRepository.GetAllUsers())
            {
                userRepository.DeleteUser(user.Nick);
            }
        }
    }
}
