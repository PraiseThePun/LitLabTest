using LitLabKata.Helpers;
using LitLabKata.Models;
using NUnit.Framework;

namespace LitLabKata.Test.Models
{
    public class UserShould
    {
        [Test]
        public void ThrowUserMissingFieldsException()
        {
            var user = new User("", "name", "surname", "email@gmail.com", "asdf", "123456789");

            Assert.Throws<UserMissingFieldsException>(() => user.Validate());
        }

        [Test]
        public void ThrowInvalidUserEmailException()
        {
            var user = new User("nick", "name", "surname", "emailgmail.com", "asdf", "123456789");

            Assert.Throws<InvalidUserEmailException>(() => user.Validate());
        }

        [Test]
        public void ThrowInvalidUserPhoneException()
        {
            var user = new User("nick", "name", "surname", "email@gmail.com", "asdf", "127573456789");

            Assert.Throws<InvalidUserPhoneException>(() => user.Validate());
        }
    }
}
