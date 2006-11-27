using System;
using System.Collections.Generic;
using System.Text;

namespace RavenClaw.MudEngine.Exceptions
{
    class MudSessionClosedException : Exception
    {
        public MudSessionClosedException() : base ()
        {

        }

        public MudSessionClosedException(string message)
            : base(message)
        {

        }

        public MudSessionClosedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
