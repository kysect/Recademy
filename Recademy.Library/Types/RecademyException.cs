using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Types
{
    public class RecademyException : Exception
    {
        public RecademyException(string message) : base(message)
        {

        }
    }
}
