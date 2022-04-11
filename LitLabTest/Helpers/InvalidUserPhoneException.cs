using System.Runtime.Serialization;

namespace LitLabKata.Helpers
{
    [Serializable]
    public class InvalidUserPhoneException : Exception
    {
        public InvalidUserPhoneException()
        {
        }

        public InvalidUserPhoneException(string? message) : base(message)
        {
        }

        public InvalidUserPhoneException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidUserPhoneException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}