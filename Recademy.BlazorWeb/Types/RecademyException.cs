using System;

namespace Recademy.BlazorWeb.Types
{
    public class RecademyException : Exception
    {
        public RecademyException(string message) : base(message)
        {

        }
    }
}
