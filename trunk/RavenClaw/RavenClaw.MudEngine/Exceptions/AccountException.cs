using System;
using System.Collections.Generic;
using System.Text;

namespace RavenClaw.MudEngine.Exceptions
{
    abstract class AccountException : Exception
    {
        public AccountException() : base()
        {

        }

        public AccountException(string message)
            : base(message)
        {

        }

        public AccountException(string message, Exception innerException) 
            : base(message, innerException)
        {

        }
    }
}
