using System;

namespace Recademy.Library.Types
{
    public class RecademyException : Exception
    {
        public RecademyException(string message) : base(message)
        {

        }

        public static RecademyException UserNotFound(int id) => new RecademyException($"User wasn't found, id: {id}");
    }
}
