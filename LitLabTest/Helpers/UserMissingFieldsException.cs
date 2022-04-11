using System.Runtime.Serialization;

namespace LitLabKata.Helpers
{
    [Serializable]
    public class UserMissingFieldsException : Exception
    {
        public UserMissingFieldsException()
        {
        }

        public UserMissingFieldsException(string? message) : base(message)
        {
        }

        public UserMissingFieldsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserMissingFieldsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}