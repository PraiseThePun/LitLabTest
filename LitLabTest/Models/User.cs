using LitLabKata.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LitLabKata.Models
{
    public class User
    {
        public User(string nick, string name, string surnames, string email, string direction, string phone)
        {
            Nick = nick;
            Name = name;
            Surnames = surnames;
            Email = email;
            Direction = direction;
            Phone = phone;
        }

        public string Nick { get; private set; }
        public string Name { get; private set; }
        public string Surnames { get; private set; }
        public string Email { get; private set; }
        public string Direction { get; private set; }
        public string Phone { get; private set; }

        public void Validate()
        {
            CheckNoFieldsAreEmpty();
            CheckEmailIsValid();
            CheckPhoneIsValid();
        }

        private void CheckNoFieldsAreEmpty()
        {
            if (string.IsNullOrEmpty(Nick) ||
                string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(Surnames) ||
                string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Direction) ||
                string.IsNullOrEmpty(Phone))
                throw new UserMissingFieldsException("User cannot contain empty properties.");
        }

        private void CheckEmailIsValid()
        {
            var emailAddressAttribute = new EmailAddressAttribute();

            if (!emailAddressAttribute.IsValid(Email))
                throw new InvalidUserEmailException("The email is not formatted propperly.");
        }

        private void CheckPhoneIsValid()
        {
            if (!Regex.Match(Phone, @"^([0-9]{9})$").Success)
                throw new InvalidUserPhoneException("The phone number is not formatted propperly.");
        }
    }
}
